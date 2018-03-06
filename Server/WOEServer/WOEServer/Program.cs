using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WOEServer
{
    class Program
    {
        private static byte[] buffer = new byte[1024];
        private static List<Socket> clientSockets = new List<Socket>();
        private static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
        }

        private void SetupServer()
        {
            Console.WriteLine("Setting up server...");
            serverSocket.Bind( new IPEndPoint( IPAddress.Any, 100 ) );
            serverSocket.Listen(1);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = serverSocket.EndAccept(AR);
            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] dataBuff = new byte[received];
            Array.Copy(buffer, dataBuff, received);

            string text = Encoding.ASCII.GetString(dataBuff);
            Console.WriteLine("Text received: " + text);

        }
    }
}
