using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliding : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			GameObject.Find ("GameManager").GetComponent<GameManagerScript> ().DecreaseHP ();
		}else if (other.tag == "meteorite") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
