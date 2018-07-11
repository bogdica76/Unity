using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class TestMPP : MonoBehaviour {

	public UdpClient client;
	public IPAddress serverIp;
	public string hostIp;
	public int hostPort;
	public IPEndPoint hostEndPoint;

	void Start(){
		serverIp = IPAddress.Parse(hostIp);
		hostEndPoint = new IPEndPoint(serverIp,hostPort);

		client = new UdpClient();
		client.Connect(hostEndPoint);
		client.Client.Blocking = false;
	}

	public void SendData(string aMsg){
		byte[] data = Encoding.UTF8.GetBytes(aMsg);
		client.Send(data, data.Length);
		client.BeginReceive(new AsyncCallback(processResponse), client);
	}

	public void processResponse(IAsyncResult res){
		try {
			byte[] dataReceived = client.EndReceive(res, ref hostEndPoint);
			Debug.Log(Encoding.UTF8.GetString(dataReceived));
		} catch (Exception ex) {
			throw ex;
		}
	}

	void OnGUI()
	{
		if(GUI.Button (new Rect (10,10,100,40), "Send"))
		{
			//DynamicObject d = new DynamicObject();
			SendData("testCommand");
		}
	}



}