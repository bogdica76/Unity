using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject playerToFollow;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (playerToFollow != null)
        {
            transform.position = playerToFollow.transform.position;
        }
        else {
            Debug.Log("CAMERA WAS NOT ASSIGNED");
        }
	}
}
