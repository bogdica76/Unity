using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int connectionID;
    private float posX, posY, posZ;

	// Use this for initialization
	void Start () {
        posX = transform.position.x;
        posY = transform.position.y;
        posZ = transform.position.z;
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

        //bogdan: trying to limit the number of packets
        //send packets only if the difference is greater than 0.2
        float vDiff = 0.2f;
        bool difX = transform.position.x >= posX + vDiff || transform.position.x <= posX - vDiff;
        bool difY = transform.position.y >= posY + vDiff || transform.position.y <= posY - vDiff;
        bool difZ = transform.position.z >= posZ + vDiff || transform.position.z <= posZ - vDiff;
        if (difX || difY || difZ)
        {
            Debug.Log("player has moved");
            posX = transform.position.x;
            posY = transform.position.y;
            posZ = transform.position.z;
            ClientTCP.SendMovement(transform.position, transform.rotation);
        }
    }
}
