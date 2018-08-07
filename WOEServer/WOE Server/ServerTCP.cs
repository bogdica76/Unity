using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace KaymakGames_Server
{
    class ServerTCP
    {
        static TcpListener serverSocket;
        public static Clients[] Client = new Clients[Constants.MAX_PLAYERS];

        public static void InitializeNetwork()
        {
            serverSocket = new TcpListener(IPAddress.Any, 5555);
            serverSocket.Start();
            serverSocket.BeginAcceptTcpClient(OnClientConnect, null);
        }
        static void OnClientConnect(IAsyncResult result)
        {
            TcpClient client = serverSocket.EndAcceptTcpClient(result);
            serverSocket.BeginAcceptTcpClient(OnClientConnect, null);
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if (Client[i].socket == null)
                {
                    Client[i].socket = client;
                    Client[i].connectionID = i;
                    Client[i].ip = client.Client.RemoteEndPoint.ToString();
                    Client[i].Start();
                    Text.WriteLog("Connection received from " + Client[i].ip + " | ConnectionID: " + Client[i].connectionID);
                    General.JoinedGame(i);
                    //SendWelcomeMessage(Client[i].connectionID);
                    return;
                }
            }
        }

        static void SendDataTo(int connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
            buffer.WriteBytes(data);
            Client[connectionID].myStream.BeginWrite(buffer.ToArray(), 0, buffer.ToArray().Length, null, null);
            buffer.Dispose();
        }

        static void SendDataToAll(byte[] data) {
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if (Client[i].socket != null)
                    SendDataTo(i, data);
            }
        }

        static void SendDataToAllBut(int connectionID, byte[] data) {
            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if(connectionID != i)
                    if (Client[i].socket != null)
                        SendDataTo(i, data);
            }
        }

        #region SendPackages
        public static void SendWelcomeMessage(int connectionID)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((long)ServerPackets.SWelcomeMessage); //Packet Identifier.
            buffer.WriteInteger(connectionID);
            buffer.WriteString("Welcome to my game server. You are connected to World 1");
            SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }

        public static void SendIngame(int connectionID) {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((long)ServerPackets.SIngame);
            buffer.WriteInteger(connectionID);
            SendDataTo(connectionID, buffer.ToArray());
            buffer.Dispose();
        }

        public static byte[] PlayerData(int connectionID) {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((long)ServerPackets.SPlayerData);
            buffer.WriteInteger(connectionID);
            return buffer.ToArray();
        }

        public static void SendInWorld(int connectionID) {
            // send all connected players to the newly connected player
            // load all map info before joining the game

            for (int i = 0; i < Constants.MAX_PLAYERS; i++)
            {
                if (Client[i].socket != null)
                {
                    if (i != connectionID)
                    {
                        SendDataTo(connectionID, PlayerData(i));
                    }
                }
            }
            //send my data to everyone else online
            //player will be instantiated when it gets intantiated on everyone else's scene
            SendDataToAll(PlayerData(connectionID));
        }

        public static void SendPlayerMove(int connectionID, float x, float y, float z, float rotx, float roty, float rotz) {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteLong((long)ServerPackets.SPlayerMove);
            buffer.WriteInteger(connectionID);

            buffer.WriteFloat(x);
            buffer.WriteFloat(y);
            buffer.WriteFloat(z);

            buffer.WriteFloat(rotx);
            buffer.WriteFloat(roty);
            buffer.WriteFloat(rotz);

            SendDataToAllBut(connectionID, buffer.ToArray());
        }
        #endregion
    }
}

