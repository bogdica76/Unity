using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour {

	public bool isCamMoving = false;
	public float speed = 2.5f;
	public float screenWidth, screenHeight = 0;
	// Use this for initialization
	void Start () {
		screenWidth = Screen.width;
		screenHeight = Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera ();
	}

	public void MoveCamera()
		{
			Vector3 camPos = transform.position;
			if (Input.mousePosition.x > screenWidth - 100)
			{
				isCamMoving = true;
				camPos.x += speed * Time.deltaTime;
			}
			else if (Input.mousePosition.x < 100)
			{
				isCamMoving = true;
				camPos.x -= speed*Time.deltaTime;
			}

			else if (Input.mousePosition.y > screenHeight - 100)
			{
				isCamMoving = true;
				camPos.z += speed*Time.deltaTime;
			}
			else if (Input.mousePosition.y < 100)
			{
				isCamMoving = true;
				camPos.z -= speed * Time.deltaTime;
			}
			else
			{
				isCamMoving = false;
			}

			transform.position = camPos ;
	}
}
