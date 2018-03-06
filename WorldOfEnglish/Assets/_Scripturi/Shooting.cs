using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

	// Use this for initialization
	public bool canFire = true;
	public GameObject bulletPrefab;
	private float fireRate = 3.0f;
	private float lastShot = 0.0f;
	private float v, h = 0.0f;

	// Add a public field for the location of the Bullet Spawn.

	public Transform bulletSpawn;
	// Update is called once per frame
	void Update ()
	{
		h += 2.0f * Input.GetAxis ("Mouse X");
		v -= 2.0f * Input.GetAxis ("Mouse Y");

		//Debug.Log (h.ToString () + " - " + v.ToString ());

		transform.eulerAngles = new Vector3(v, h, 0);
		//transform.Rotate (0, h, 0);

		bool hasBullets = GameObject.Find ("GameManager").GetComponent<BigGameManager> ().checkForAmmo ("bullets");
		if (Input.GetKeyDown (KeyCode.Space) && canFire && hasBullets) {
			Fire ();

		}

	}

	void Fire ()
	{
		if (Time.time > fireRate + lastShot) {
			lastShot = Time.time;
			// Create the Bullet from the Bullet Prefab
			var bullet = /*(GameObject)Instantiate (
				                      bulletPrefab,
				                      bulletSpawn.position,
				                      bulletSpawn.rotation);*/

				(GameObject)Instantiate (bulletPrefab,
					GameObject.Find ("creatorGlont").transform.position,
					transform.rotation);
			GameObject.Find ("GameManager").GetComponent<BigGameManager> ().shootAmmo ("bullets");
			// Add velocity to the bullet
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 30;

			// Destroy the bullet after 2 seconds
			Destroy (bullet, 10.0f);

		}
		 
	}

}
