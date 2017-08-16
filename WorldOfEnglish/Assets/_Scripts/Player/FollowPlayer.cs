using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	GameObject aPlayer;

	// Use this for initialization
	void Start () {
		aPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = aPlayer.transform.position;
		transform.rotation = aPlayer.transform.rotation;
	}
}
