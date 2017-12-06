using UnityEngine;

public class MouseLook : MonoBehaviour {

	public float speed = 10.0F;

	void Start () {
		Cursor.lockState = CursorLockMode.Locked; 
	}

	void Update () {
		float translation = Input.GetAxis("Vertical") * speed;
		float straffe = Input.GetAxis("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

		transform.Translate(straffe, 0, translation);

		if (Input.GetKeyDown("escape"))
			Cursor.lockState = CursorLockMode.None;
	}
}﻿