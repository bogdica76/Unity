﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using WOEServer.Common;
//using TestPhotonLib.Common;
using UnityEngine;

public class PhotonServer : MonoBehaviour, IPhotonPeerListener
{
    private const string CONNECTION_STRING = "bogdica.go.ro:4530";
    private const string APP_NAME = "WOEServer";

    private static PhotonServer _instance;
    public static PhotonServer Instance
    {
        get { return _instance; }
    }

    public List<Player> Players = new List<Player>();

    private PhotonPeer PhotonPeer { get; set; }

    public string CharacterName { get; set; }

    public event EventHandler<LoginEventArgs> OnLoginResponse;
    //public event EventHandler<ChatMessageEventArgs> OnReceiveChatMessage;

    void Awake()
    {
        if (Instance != null)
        {
            DestroyObject(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        Application.runInBackground = true;

        _instance = this;
    }
    // Use this for initialization
    void Start()
    {

        PhotonPeer = new PhotonPeer(this, ConnectionProtocol.Tcp);
        if (!PhotonPeer.RegisterType(typeof(Vector3Net), (byte)200, Vector3Net.Serialize, Vector3Net.Deserialize))
        {
            throw new Exception("not working...");
        }
        Connect();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonPeer != null)
            PhotonPeer.Service();
    }

    void OnApplicationQuit()
    {
        WorldExitOperation();
        Disconnect();
    }

    private void Connect()
    {
        if (PhotonPeer != null)
            PhotonPeer.Connect(CONNECTION_STRING, APP_NAME);
    }

    private void Disconnect()
    {        
        if (PhotonPeer != null)
            PhotonPeer.Disconnect();
    }

    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log("DebugReturn level:" + level.ToString());
    }

    public void OnOperationResponse(OperationResponse operationResponse)
    {
        switch (operationResponse.OperationCode)
        {
            case (byte)OperationCodes.Login:
                Debug.Log("mama");
                LoginHandler(operationResponse);
                break;
            /*case (byte)OperationCode.ListPlayers:
                ListPlayersHandler(operationResponse);
                break;*/
            default:
                Debug.Log("Unknown OperationResponse:" + operationResponse.OperationCode);
                break;
        }
    }

    public void OnEvent(EventData eventData)
    {
        /*switch (eventData.Code)
        {
            case (byte)EventCode.ChatMessage:
                ChatMessageHandler(eventData);
                break;
            case (byte)EventCode.Move:
                MoveHandler(eventData);
                break;
            case (byte)EventCode.WorldEnter:
                WorldEnterHandler(eventData);
                break;
            case (byte)EventCode.WorldExit:
                WorldExitHandler(eventData);
                break;
            default:
                Debug.Log("Unknown Event:" + eventData.Code);
                break;
        }*/
    }

    public void OnStatusChanged(StatusCode statusCode)
    {
        switch (statusCode)
        {
            case StatusCode.Connect:
                Debug.Log("Connected to server!");
                break;
            case StatusCode.Disconnect:
                Debug.Log("Disconnected from server!");
                break;
            case StatusCode.TimeoutDisconnect:
                Debug.Log("TimeoutDisconnected from server!");
                break;
            case StatusCode.DisconnectByServer:
                Debug.Log("DisconnectedByServer from server!");
                break;
            case StatusCode.DisconnectByServerUserLimit:
                Debug.Log("DisconnectedByLimit from server!");
                break;
            case StatusCode.DisconnectByServerLogic:
                Debug.Log("DisconnectedByLogic from server!");
                break;
            case StatusCode.EncryptionEstablished:
                break;
            case StatusCode.EncryptionFailedToEstablish:
                break;
            default:
                Debug.Log("Unknown status:" + statusCode.ToString());
                break;
        }
    }

    #region handlers for response

    private void LoginHandler(OperationResponse operationResponse)
    {
        if (operationResponse.ReturnCode != 0)
        {
            Debug.Log("A aparut o eroare: " + operationResponse.ReturnCode);
            /*ErrorCodes errorCode = (ErrorCodes)operationResponse.ReturnCode;
            switch (errorCode)
            {
                case ErrorCodes.NameIsExist:
                    if (OnLoginResponse != null)
                        OnLoginResponse(this, new LoginEventArgs(ErrorCodes.NameIsExist));
                    break;
                default:
                    Debug.Log("Error Login returnCode:" + operationResponse.ReturnCode);
                    break;
            }

            return;*/
        }
        else {
            Debug.Log("Login succesful");
        }

        if (OnLoginResponse != null)
            OnLoginResponse(this, new LoginEventArgs(ErrorCodes.Ok));
    }

 /*   
    private void ChatMessageHandler(EventData eventData)
    {
        string message = (string)eventData.Parameters[(byte)ParameterCodes.ChatMessage];

        if (OnReceiveChatMessage != null)
            OnReceiveChatMessage(this, new ChatMessageEventArgs(message));
    }
    */

    private void MoveHandler(EventData eventData)
    {
        string characterName = (string)eventData.Parameters[(byte)ParameterCodes.CharacterName];
        float posX = (float)eventData.Parameters[(byte)ParameterCodes.PosX];
        float posY = (float)eventData.Parameters[(byte)ParameterCodes.PosY];
        float posZ = (float)eventData.Parameters[(byte)ParameterCodes.PosZ];

        var client = Players.FirstOrDefault(n => n.CharacterName.Equals(characterName));
        if (client == null)
        {
            Debug.Log("Move:not client");
            return;
        }


        client.NewPosition = new Vector3(posX, posY, posZ);

        Debug.Log("MoveHandler");
    }

    private void WorldEnterHandler(EventData eventData)
    {
        string characterName = (string)eventData.Parameters[(byte)ParameterCodes.CharacterName];
        float posX = (float)eventData.Parameters[(byte)ParameterCodes.PosX];
        float posY = (float)eventData.Parameters[(byte)ParameterCodes.PosY];
        float posZ = (float)eventData.Parameters[(byte)ParameterCodes.PosZ];

        var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.position = new Vector3(posX, posY, posZ);

        var player = obj.AddComponent<Player>();
        player.CharacterName = characterName;

        Players.Add(player);

        Debug.Log("WorldEnterHandler charName:" + characterName);
    }

    private void WorldExitHandler(EventData eventData)
    {
        string characterName = (string)eventData.Parameters[(byte)ParameterCodes.CharacterName];

        var client = Players.FirstOrDefault(n => n.CharacterName.Equals(characterName));
        if (client == null)
            return;
        Players.Remove(client);
        DestroyObject(client.gameObject);

        Debug.Log("WorldExitHandler charName:" + characterName);
    }

    private void ListPlayersHandler(OperationResponse operationResponse)
    {
        var dicPlayer = operationResponse.Parameters[(byte)ParameterCodes.ListPlayers] as Dictionary<string, object[]>;
        if (dicPlayer == null)
        {
            Debug.Log("ListPlayers null!");
            return;
        }

        foreach (var p in dicPlayer)
        {
            string charName = p.Key;
            object[] pos = p.Value;
            var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            obj.transform.position = new Vector3((float)pos[0], (float)pos[1], (float)pos[2]);

            var player = obj.AddComponent<Player>();
            player.CharacterName = charName;

            Players.Add(player);

            Debug.Log("Create player from list charName:" + charName);
        }

        Debug.Log("ListPlayersHandler ");
    }
    #endregion


    #region Up-level API

    public void SendLoginOperation(string name)
    {
        PhotonPeer.OpCustom((byte)OperationCodes.Login,
                            new Dictionary<byte, object> { { (byte)ParameterCodes.CharacterName, "tata" } }, true);
    }

    public void SendChatMessage(string message)
    {
        PhotonPeer.OpCustom((byte)OperationCodes.SendChatMessage,
                           new Dictionary<byte, object> { { (byte)ParameterCodes.ChatMessage, message } }, true);
    }

    public void GetRecentChatMessage()
    {
        PhotonPeer.OpCustom((byte)OperationCodes.GetRecentChatMessages, new Dictionary<byte, object>(), true);
    }

    public void MoveOperation(float x, float y, float z)
    {
        PhotonPeer.OpCustom((byte)OperationCodes.Move,
                            new Dictionary<byte, object>
                                {
                                    {(byte) ParameterCodes.PosX, x},
                                    {(byte) ParameterCodes.PosY, y},
                                    {(byte) ParameterCodes.PosZ, z},
                                }, false);
    }

    public void WorldEnterOperation()
    {
        PhotonPeer.OpCustom((byte)OperationCodes.WorldEnter, new Dictionary<byte, object>(), true);
    }

    public void WorldExitOperation()
    {
        PhotonPeer.OpCustom((byte)OperationCodes.WorldExit, new Dictionary<byte, object>(), true);
    }

    public void ListPlayersOperation()
    {
        PhotonPeer.OpCustom((byte)OperationCodes.ListPlayers, new Dictionary<byte, object>(), true);
    }
    #endregion
}
