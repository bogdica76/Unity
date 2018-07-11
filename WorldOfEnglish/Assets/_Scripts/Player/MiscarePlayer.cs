using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscarePlayer : MonoBehaviour {

	public float speed = 15.0f;
	public float rotationSpeed = 150.0f;

	void Start(){

	}

void Update()
    {
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

    }
}
