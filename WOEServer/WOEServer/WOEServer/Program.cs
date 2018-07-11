using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Globalization;

namespace WOEServer
{

    class Program
    {
        //public static ServerAction server;

        static void Main(string[] args)
        {
            CultureInfo customCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;


            int recv;
            byte[] data = new byte[1024];
            string vReceivedCommand;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 6321);

            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            newsock.Bind(ipep);
            Console.WriteLine("Waiting for a client...");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)(sender);

            recv = newsock.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Message received from {0}:", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref Remote);
                vReceivedCommand = Encoding.ASCII.GetString(data, 0, recv);
                switch (vReceivedCommand) {
                    case "testCommand":
                        data = Encoding.UTF8.GetBytes("Test command was received");
                        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
                        break;
                    default:
                        data = Encoding.UTF8.GetBytes("Another command was received");
                        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
                        break;
                }

                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
            //server = new ServerAction();
        }

    }
    /*
    public class ServerAction
    {
        private int port = 6321;

        private List<ServerClient> clients = new List<ServerClient>();
        private List<ServerClient> disconnectList = new List<ServerClient>();
        private TcpListener server;
        private bool serverStarted;
        private List<Unit> units = new List<Unit>();
        private bool ResyncNeeded = false;

        //the constructor, adds the listener
        public ServerAction()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                StartListening();
                serverStarted = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("\r\n" + e.Message);
                //Program.form.DebugTextBox.Text += "\r\n" + e.Message;
            }
        }


        //called at every fixed time intervals => time can be adjusted at timer component's property
        //used to check if there's incoming data
        public void Update()
        {
            if (!serverStarted)
                return;

            foreach (ServerClient c in clients)
            {
                // Is the client still connected?
                if (!IsConnected(c.tcp))
                {
                    c.tcp.Close();
                    disconnectList.Add(c);
                    continue;
                }
                else
                {
                    NetworkStream s = c.tcp.GetStream();
                    if (s.DataAvailable)
                    {
                        StreamReader reader = new StreamReader(s, true);
                        string data = reader.ReadLine();

                        if (data != null)
                            OnIncomingData(c, data);
                    }
                }
            }

            //checking disconnected players
            for (int i = 0; i < disconnectList.Count - 1; i++)
            {
                for (int j = 0; j < units.Count; j++)
                {
                    if (units[j].clientName == disconnectList[i].clientName)
                    {
                        units.RemoveAt(j);
                    }
                }

                Console.WriteLine("\r\nUser disconnected:" + disconnectList[i].clientName);
                //Program.form.DebugTextBox.Text += "\r\nUser disconnected:" + disconnectList[i].clientName;

                clients.Remove(disconnectList[i]);
                disconnectList.RemoveAt(i);
                ResyncNeeded = true;
            }
            //if some1 disconnected, tell it to other players as well.
            if (ResyncNeeded)
            {
                SynchronizeUnits();
                ResyncNeeded = false;
            }
        }

        private void StartListening()
        {
            server.BeginAcceptTcpClient(AcceptTcpClient, server);
        }

        private void AcceptTcpClient(IAsyncResult ar)
        {
            TcpListener listener = (TcpListener)ar.AsyncState;

            string allUsers = "";
            foreach (ServerClient i in clients)
            {
                allUsers += i.clientName + '|';
            }

            ServerClient sc = new ServerClient(listener.EndAcceptTcpClient(ar));
            clients.Add(sc);

            StartListening();
            //request authentication from client
            Broadcast("WhoAreYou|", clients[clients.Count - 1]);
        }

        private bool IsConnected(TcpClient c)
        {
            try
            {
                if (c != null && c.Client != null && c.Client.Connected)
                {
                    if (c.Client.Poll(0, SelectMode.SelectRead))
                        return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);

                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        // Server Send
        private void Broadcast(string data, List<ServerClient> cl)
        {
            foreach (ServerClient sc in cl)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(sc.tcp.GetStream());
                    writer.WriteLine(data);
                    writer.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\r\n" + e.Message);
                    //Program.form.DebugTextBox.Text += "\r\n" + e.Message;
                }
            }
        }

        private void Broadcast(string data, ServerClient c)
        {
            List<ServerClient> sc = new List<ServerClient> { c };
            Broadcast(data, sc);
        }

        // Server Read
        private void OnIncomingData(ServerClient c, string data)
        {
            string[] aData = data.Split('|');

            //login
            if (c.clientName != null)
            {
                Console.WriteLine("\r\nClient '" + c.clientName + "' sent command: " + data);
                //Program.form.DebugTextBox.Text += "\r\nClient '" + c.clientName + "' sent command: " + data;
            }
            else
            {
                Console.WriteLine("\r\nNew Client trying to join server. Requesting authentication.");
                //Program.form.DebugTextBox.Text += "\r\nNew Client trying to join server. Requesting authentication.";


                if (aData[0] == "Iam")
                {
                    bool authenticated = Database.AuthenticateUser(aData[1], aData[2]);
                    if (authenticated)
                    {
                        foreach (ServerClient client in clients)
                        {
                            if (aData[1] == client.clientName)
                            {
                                Console.WriteLine("\r\nThis user is already connected");
                                //Program.form.DebugTextBox.Text += "\r\nThis user is already connected";

                                c.tcp.Close();
                                disconnectList.Add(c);
                                return;
                            }
                        }
                        c.clientName = aData[1];

                        Console.WriteLine("\r\nUser authenticated");
                        //Program.form.DebugTextBox.Text += "\r\nUser authenticated";

                        Broadcast("Authenticated|", c);
                    }
                    else
                    {
                        Console.WriteLine("\r\nUser authentication failed, client disconnected.");
                        //Program.form.DebugTextBox.Text += "\r\nUser authentication failed, client disconnected.";
                        c.tcp.Close();
                        disconnectList.Add(c);
                    }
                    return;
                }
            }


            //gameplay commands
            switch (aData[0])
            {
                case "SynchronizeRequest":
                    SynchronizeUnits(c);
                    break;
                case "SpawnUnit":
                    Unit unit = new Unit();

                    unit.clientName = c.clientName;
                    //give a new ID to the new units
                    int newid = 0;
                    foreach (Unit u in units)
                    {
                        if (u.unitID >= newid) { newid = u.unitID + 1; }
                    }

                    unit.unitID = newid;
                    unit.unitPositionX = 0.0f;
                    unit.unitPositionY = 0.0f;
                    unit.unitPositionZ = 0.0f;
                    units.Add(unit);
                    Broadcast("UnitSpawned|" + c.clientName + "|" + unit.unitID + "|" + unit.unitPositionX + "|" + unit.unitPositionY + "|" + unit.unitPositionZ, clients);
                    break;
                case "Moving":
                    Broadcast("UnitMoved|" + c.clientName + "|" + aData[1] + "|" + aData[2] + "|" + aData[3] + "|" + aData[4], clients);
                    int id;
                    Int32.TryParse(aData[1], out id);
                    float parsedX;
                    float parsedY;
                    float parsedZ;
                    float.TryParse(aData[2], out parsedX);
                    float.TryParse(aData[3], out parsedY);
                    float.TryParse(aData[4], out parsedZ);
                    foreach (Unit u in units)
                    {
                        if (u.unitID == id)
                        {
                            u.unitPositionX = parsedX;
                            u.unitPositionY = parsedY;
                            u.unitPositionZ = parsedZ;
                        }
                    }
                    Console.WriteLine("\r\n" + parsedX + "  " + parsedY + "  " + parsedZ);
                    //Program.form.DebugTextBox.Text += "\r\n" + parsedX + "  " + parsedY + "  " + parsedZ;
                    break;
                default:
                    Console.WriteLine("\r\nReceived unknown signal => skipping");
                    //Program.form.DebugTextBox.Text += "\r\nReceived unknown signal => skipping";
                    break;
            }
        }

        //syncing 1 client
        private void SynchronizeUnits(ServerClient c)
        {
            string dataToSend = "Synchronizing|" + units.Count;
            foreach (Unit u in units)
            {
                dataToSend += "|" + (u.unitID) + "|" + u.unitPositionX + "|" + u.unitPositionY + "|" + u.unitPositionZ;
            }
            Broadcast(dataToSend, c);
            Console.WriteLine("\r\nSynchronization request sent: " + dataToSend);
            //Program.form.DebugTextBox.Text += "\r\nSynchronization request sent: " + dataToSend;
        }

        //syncing all clients
        private void SynchronizeUnits()
        {
            string dataToSend = "Synchronizing|" + units.Count;
            foreach (Unit u in units)
            {
                dataToSend += "|" + (u.unitID) + "|" + u.unitPositionX + "|" + u.unitPositionY + "|" + u.unitPositionZ;
            }
            Broadcast(dataToSend, clients);
            Console.WriteLine("\r\nSynchronization request sent: " + dataToSend);
            //Program.form.DebugTextBox.Text += "\r\nSynchronization request sent: " + dataToSend;
        }
    }

    public class ServerClient
    {
        public string clientName;
        public TcpClient tcp;
        public ServerClient(TcpClient tcp)
        {
            this.tcp = tcp;
        }
    }

    public class Unit
    {
        public string clientName;
        public int unitID;
        public float unitPositionX;
        public float unitPositionY;
        public float unitPositionZ;
    }

    //NOTE: never store sensitive user data like passwords like this in an actual product. use something like hashing... 
    public static class Database
    {
        public static bool AuthenticateUser(string username, string password)
        {
            int result = 0;
            //result = (int)Program.form.usersTableAdapter.Authenticate(username, password);
            if (result == 1)
            {
                return true;
            }
            else return false;
        }
    }
    */
}
