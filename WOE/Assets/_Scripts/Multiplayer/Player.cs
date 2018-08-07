using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int connectionID;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (connectionID != Globals.myConnectionID) {
            return;
		}

		Move ();
	}

    void Move() {
		var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		var z = Input.GetAxis ("Vertical") * Time.deltaTime * 4.0f;

		transform.Rotate (0.0f, x, 0.0f);
		transform.Translate (0.0f, 0.0f, z);

        ClientTCP.SendMovement(transform.position, transform.rotation);
    }
}
