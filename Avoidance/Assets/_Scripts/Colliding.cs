using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliding : MonoBehaviour {

    public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

        if (other.tag == "Player") {
			GameObject.Find ("Player").GetComponent<PlayerShipSetup> ().DecreaseHP ();
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        else if (other.tag == "meteorite") {
			Destroy (other.gameObject);
			Destroy (gameObject);
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
    }
}
