using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	public float x = 0.0f;
	public float y = 0.0f;
	public float z = 0.0f;

	// Use this for initialization
	void Start () {
		
	}

	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (x, y, z) * Time.deltaTime);
	}
}
