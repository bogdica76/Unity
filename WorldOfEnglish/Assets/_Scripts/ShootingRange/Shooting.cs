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
	public Transform bulletSpawn;

	void Start(){
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update ()
	{
		h += 2.0f * Input.GetAxis ("Mouse X");
		v -= 2.0f * Input.GetAxis ("Mouse Y");

		transform.eulerAngles = new Vector3(v, h, 0);

		bool hasBullets = GameObject.Find ("GameManager").GetComponent<BigGameManager> ().checkForAmmo ("bullets");
		if (Input.GetKeyDown (KeyCode.Space) && canFire && hasBullets) {
			Fire ();
		}
	}

	void Fire ()
	{
		if (Time.time > fireRate + lastShot) {
			lastShot = Time.time;

			var bullet = (GameObject)Instantiate (
				bulletPrefab,
				bulletSpawn.position,
				bulletSpawn.rotation);

			GameObject.Find ("GameManager").GetComponent<BigGameManager> ().shootAmmo ("bullets");
			bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * 30;

			Destroy (bullet, 10.0f);
		}		 
	}
}
