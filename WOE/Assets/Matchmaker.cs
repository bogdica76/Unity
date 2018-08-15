using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matchmaker : Photon.PunBehaviour
{
    private PhotonView myPhotonView;

    // Use this for initialization
    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        // when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        GameObject monster = PhotonNetwork.Instantiate("PhotonTestPlayer", new Vector3(250.0f, 10.0f, 250.0f), Quaternion.identity, 0);
        monster.GetComponent<myThirdPersonController>().isControllable = true;
        //Debug.Log("trying to setup camera");
        //monster.GetComponent<CameraWork>().followOnStart = true;
        //Debug.Log("after setup");
        myPhotonView = monster.GetComponent<PhotonView>();

        monster.transform.GetChild(0).gameObject.active = true;
    }
}
