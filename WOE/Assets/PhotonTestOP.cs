using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonTestOP : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
        PhotonNetwork.networkingPeer.OpCustom(1, new Dictionary<byte, object>{ { 1, "nume"} }, false );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
