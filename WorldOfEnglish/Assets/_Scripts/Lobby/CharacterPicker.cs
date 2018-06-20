using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPicker : MonoBehaviour {
	public GameObject pickedMark;

	void Update () {
		if (Input.GetMouseButtonDown(0))
		{			
			RaycastHit hitInfo = new RaycastHit();
			bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (hit) 
			{
				//eliberam orice alte puncte de miscare/interactiune
				GameObject[] oldDestinationPoints =  GameObject.FindGameObjectsWithTag("destinationPoint");

				foreach(GameObject destination in oldDestinationPoints)
				{
					Destroy (destination);
				}

				if (hitInfo.transform.gameObject.tag == "interactable")
				{
					GameObject interactedObject = hitInfo.transform.gameObject;
					var pickedMarkObject = (GameObject)Instantiate (
						pickedMark,
						interactedObject.transform.position,
						pickedMark.transform.rotation);
				}
			}
		}
	}
}
