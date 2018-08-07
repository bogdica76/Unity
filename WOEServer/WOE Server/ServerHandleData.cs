using System;
using System.Collections.Generic;

namespace KaymakGames_Server
{
    public class ServerHandleData
    {
        private delegate void Packet_(long connectionID, byte[] data);
        static Dictionary<long, Packet_> packets;
        static long pLength;

        public static void InitializePackets()
        {
            Text.WriteDebug("Initializing Network Packets...");
            packets = new Dictionary<long, Packet_>
            {
                { (long)ClientPackets.CThankYouMessage, Packet_ThankYouMessage },
                { (long)ClientPackets.CMovement, Packet_Movement }
            };
        }

        public static void HandleData(long connectionID, byte[] data)
        {
            byte[] Buffer;
            Buffer = (byte[])data.Clone();

            if (ServerTCP.Client[connectionID].playerBuffer == null)
            {
                ServerTCP.Client[connectionID].playerBuffer = new ByteBuffer();
            }
            ServerTCP.Client[connectionID].playerBuffer.WriteBytes(Buffer);

            if (ServerTCP.Client[connectionID].playerBuffer.Count() == 0)
            {
                ServerTCP.Client[connectionID].playerBuffer.Clear();
                return;
            }

            if (ServerTCP.Client[connectionID].playerBuffer.Length() >= 8)
            {
                pLength = ServerTCP.Client[connectionID].playerBuffer.ReadLong(false);
                if (pLength <= 0)
                {
                    ServerTCP.Client[connectionID].playerBuffer.Clear();
                    return;
                }
            }

            while (pLength > 0 & pLength <= ServerTCP.Client[connectionID].playerBuffer.Length() - 8)
            {
                if (pLength <= ServerTCP.Client[connectionID].playerBuffer.Length() - 8)
                {
                    ServerTCP.Client[connectionID].playerBuffer.ReadLong();
                    data = ServerTCP.Client[connectionID].playerBuffer.ReadBytes((int)pLength);
                    HandleDataPackets(connectionID, data);
                }

                pLength = 0;

                if (ServerTCP.Client[connectionID].playerBuffer.Length() >= 8)
                {
                    pLength = ServerTCP.Client[connectionID].playerBuffer.ReadLong(false);

                    if (pLength < 0)
                    {
                        ServerTCP.Client[connectionID].playerBuffer.Clear();
                        return;
                    }
                }
            }
        }
        static void HandleDataPackets(long connectionID, byte[] data)
        {
            long packetIdentifier;
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);
            packetIdentifier = buffer.ReadLong();
            buffer.Dispose();
            Packet_ packet; 

            if (packets.TryGetValue(packetIdentifier, out packet))
            {
                packet.Invoke(connectionID, data);
            }
        }

        static void Packet_ThankYouMessage(long connectionID, byte[] data)
        {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);

            long packetIdentifier = buffer.ReadLong();
            string msg = buffer.ReadString();

            Console.WriteLine(msg);

            buffer.Dispose();
        }

        static void Packet_Movement(long connectionID, byte[] data) {
            ByteBuffer buffer = new ByteBuffer();
            buffer.WriteBytes(data);

            long packetIdentifier = buffer.ReadLong();

            float x = buffer.ReadFloat();
            float y = buffer.ReadFloat();
            float z = buffer.ReadFloat();

            float rotx = buffer.ReadFloat();
            float roty = buffer.ReadFloat();
            float rotz = buffer.ReadFloat();

            ServerTCP.SendPlayerMove((int)connectionID, x, y, z, rotx, roty, rotz);
        }
    }
}
