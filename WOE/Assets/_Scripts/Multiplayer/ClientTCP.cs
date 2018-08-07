using System;
using System.Net.Sockets;
using UnityEngine;

public class ClientTCP : MonoBehaviour
{
    public static ClientTCP instance;

    public TcpClient client;
    public static NetworkStream myStream;
    private byte[] asyncBuffer;
    public bool isConnected;

    public byte[] receivedBytes;
    public bool handleData = false;

    private string IP_ADDRESS = "127.0.0.1";
    private int PORT = 5555;

    public int myConnectionID;

    private void Awake()
    {
        UnityThread.initUnityThread();
        instance = this;
    }

    public void Connect()
    {
        Debug.Log("Trying to Connect to the server.");
        client = new TcpClient
        {
            ReceiveBufferSize = 4096,
            SendBufferSize = 4096
        };
        asyncBuffer = new byte[8192];
        try
        {
            client.BeginConnect(IP_ADDRESS, PORT, new AsyncCallback(ConnectCallback), client);
        }
        catch
        {
            Debug.Log("Unable to connect to server.");
        }
    }

    private void ConnectCallback(IAsyncResult result)
    {
        try
        {
            client.EndConnect(result);
            if (client.Connected == false) { return; }
            else
            {
                myStream = client.GetStream();
                myStream.BeginRead(asyncBuffer, 0, 8192, OnReceiveData, null);
                isConnected = true;
                Debug.Log("You are connected to the server sucessfully.");
            }
        }
        catch (Exception)
        {

            isConnected = false;
            return;
        }
    }

    private void OnReceiveData(IAsyncResult result)
    {
        try
        {
            int packetLength = myStream.EndRead(result);
            receivedBytes = new byte[packetLength];
            Buffer.BlockCopy(asyncBuffer, 0, receivedBytes, 0, packetLength);

            if (packetLength == 0)
            {
                Debug.Log("Disconnected.");
                UnityThread.executeInUpdate(() =>
                {
                    Application.Quit();
                });
                return;
            }
            UnityThread.executeInUpdate(() =>
            {
                ClientHandleData.HandleData(receivedBytes);
            });
            myStream.BeginRead(asyncBuffer, 0, 8192, OnReceiveData, null);

        }
        catch (Exception)
        {
            Debug.Log("Disconnected.");
            UnityThread.executeInUpdate(() =>
            {
                Application.Quit();
            });
            return;
        }
    }

    public static void SendData(byte[] data)
    {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteLong((data.GetUpperBound(0) - data.GetLowerBound(0)) + 1);
        buffer.WriteBytes(data);
        myStream.Write(buffer.ToArray(), 0, buffer.ToArray().Length);
    }

    public static void SendMovement(Vector3 pos, Quaternion rot) {
        ByteBuffer buffer = new ByteBuffer();
        buffer.WriteLong((long)ClientPackets.CMovement);

        //position
        buffer.WriteFloat(pos.x);
        buffer.WriteFloat(pos.y);
        buffer.WriteFloat(pos.z);

        //rotation
        buffer.WriteFloat(General.UnwrapAngle(rot.x));
        buffer.WriteFloat(General.UnwrapAngle(rot.y));
        buffer.WriteFloat(General.UnwrapAngle(rot.z));

        SendData(buffer.ToArray());
        buffer.Dispose();
    }
}