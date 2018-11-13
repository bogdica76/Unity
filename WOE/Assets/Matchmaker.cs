using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Matchmaker : Photon.PunBehaviour
{
    private PhotonView myPhotonView;

    void Awake()
    {
        //SpawnIntoWorld();
    }
    // Use this for initialization
    public void Start()
    {
        //PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public void ConnectToRoomCustom()
    {
        Debug.Log("connectiing to custom room");
        //SceneManager.LoadScene("TheBeginning");
        //PhotonNetwork.ConnectUsingSettings("0.1");
        //PhotonNetwork.JoinOrCreateRoom("TheBeggining", null, null);
    }

    
    public override void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connect to master");
        // when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed()
    {
        Debug.Log("failed to join room");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");
        GameObject monster = PhotonNetwork.Instantiate("PhotonTestPlayer", new Vector3(250.0f, 10.0f, 250.0f), Quaternion.identity, 0);
        monster.GetComponent<MiscarePlayer>().isControllable = true;
        //Debug.Log("trying to setup camera");
        //monster.GetComponent<CameraWork>().followOnStart = true;
        //Debug.Log("after setup");
        myPhotonView = monster.GetComponent<PhotonView>();

        monster.transform.GetChild(0).gameObject.active = true;
    }

    public void SpawnIntoWorld()
    {
        Debug.Log("joined room");
        GameObject monster = PhotonNetwork.Instantiate("PhotonTestPlayer", new Vector3(250.0f, 10.0f, 250.0f), Quaternion.identity, 0);
        monster.GetComponent<MiscarePlayer>().isControllable = true;
        //Debug.Log("trying to setup camera");
        //monster.GetComponent<CameraWork>().followOnStart = true;
        //Debug.Log("after setup");
        myPhotonView = monster.GetComponent<PhotonView>();

        monster.transform.GetChild(0).gameObject.active = true;
    }
}
