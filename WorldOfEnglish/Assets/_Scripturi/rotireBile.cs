using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotireBile : MonoBehaviour {

	void Update () 
	{
		transform.Rotate (new Vector3 (0, 30, 0) * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			GameObject.Find ("GameManager").GetComponent<BigGameManager> ().collectBullets ();
			gameObject.SetActive(false);


		}
	}
}
