using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : NetworkBehaviour {

	public Transform playerPrefab;

	private string registeredServerName = "Bogdica19_Server";
	//private bool isRefreshing = false;
	private float refreshRequestLength = 3.0f;
	private HostData[] hostData;

	private void StartServer(){
		Network.InitializeServer (16, 25002, true);
		MasterServer.RegisterHost(registeredServerName, "Test unity master server by bogdica", "test server multiplayer");
	}

	void OnMasterServerEvent(MasterServerEvent masterServerEvent)
	{
		if(masterServerEvent == MasterServerEvent.RegistrationSucceeded)
			Debug.Log("Registration Successful");
	}

	#region onGUI
/*	public void OnGUI(){
		if (Network.isServer)
			return;

		if (Network.isClient) {
			if(GUI.Button(new Rect(20f,20f,150f,30f),"Disconnect"))
			{
				Debug.Log("Disconnecting...");
			}
			return;
		}

		if(GUI.Button(new Rect(20f,20f,150f,30f),"Start New Server"))
		{
			StartServer();
		}
		if(GUI.Button(new Rect(25f,65f,150f,30f),"Refresh Server List"))
		{
			StartCoroutine("RefreshHostList");
		}

		if(hostData != null)
		{
			for(int i = 0; i< hostData.Length;i++)
			{
				if(GUI.Button(new Rect(Screen.width / 2, 65f + (30f * i),300f,30f),hostData[i].gameName))
				{
					Network.Connect (hostData [i].guid);
				}
			}
		}

	}*/
	#endregion

	public IEnumerator RefreshHostList(){
		Debug.Log ("Refreshing...");

		//cer lista de servere pentru jocul meu
		MasterServer.RequestHostList (registeredServerName);

		float timeStarted = Time.time;
		float timeEnd = Time.time + refreshRequestLength;

		while (Time.time < timeEnd) {
			//aduc lista de servere in hostData
			hostData = MasterServer.PollHostList ();
			yield return new WaitForEndOfFrame ();
		}

		if (hostData == null || hostData.Length == 0)
			Debug.Log ("No active servers have been found");
		else 
			Debug.Log (hostData.Length + " have been found");
	}

	private void SpawnPlayer(){
		Debug.Log ("Spawning player...");
		var vPosition = new Vector3 (1.0f, 5.0f, 1.0f);
		Network.Instantiate (playerPrefab, vPosition, playerPrefab.rotation, 0);
	}

	#region Events

	void OnServerInitialized(){
		Debug.Log ("Server initialized...");
		SpawnPlayer ();
	}

	void OnPlayerDisconnected(NetworkPlayer player){
		Network.RemoveRPCs (player);
		Network.DestroyPlayerObjects (player);
	}

	void OnApplicationQuit(){
		if (Network.isServer) {
			Network.Disconnect (200);
			MasterServer.UnregisterHost ();
		}

		if (Network.isClient)
			Network.Disconnect (200);
	}

	#endregion

}
