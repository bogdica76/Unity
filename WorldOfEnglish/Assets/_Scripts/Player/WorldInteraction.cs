using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WorldInteraction : MonoBehaviour {
	NavMeshAgent playerAgent;
	public GameObject destionationPoint;

	void Awake(){
/*		if (PlayerPrefs.HasKey ("posX") && PlayerPrefs.HasKey ("posY") && PlayerPrefs.HasKey ("posZ")) {
			gameObject.transform.position = new Vector3 (
				PlayerPrefs.GetFloat ("posX"),
				PlayerPrefs.GetFloat ("posY"),
				PlayerPrefs.GetFloat ("posZ")
			);
		}	

		if (PlayerPrefs.HasKey ("rotX") && PlayerPrefs.HasKey ("rotY") && PlayerPrefs.HasKey ("rotZ")) {
			gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler (
				new Vector3(
					PlayerPrefs.GetFloat ("rotX"),
					PlayerPrefs.GetFloat ("rotY"),
					PlayerPrefs.GetFloat ("rotZ")
				)
			);
		}*/
	}

	void Start(){
		playerAgent = GetComponent<NavMeshAgent> ();
	}

	private bool IsPointerOverUIObject() {
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
		eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}

	void Update () {
	/*	#if UNITY_ANDROID
		Debug.Log("android");
		if (
			Input.GetMouseButtonDown (0) && 
			!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)
		)
		{
			GetInteraction ();
			return;
		}
		#endif*/


		if (
			Input.GetMouseButtonDown (0) && 
			!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
			!IsPointerOverUIObject()
			)
		{
			GetInteraction ();
		}

	}

	void GetInteraction(){
		Ray interactionRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit interactionInfo;
		if ( Physics.Raycast (interactionRay, out interactionInfo, Mathf.Infinity) ) {
			GameObject interactedObject = interactionInfo.collider.gameObject;

			//eliberam orice alte puncte de miscare/interactiune
			GameObject[] oldDestinationPoints =  GameObject.FindGameObjectsWithTag("destinationPoint");

			foreach(GameObject destination in oldDestinationPoints)
			{
				Destroy (destination);
			}

			if (interactedObject.tag == "interactable") {
				Debug.Log ("Interacted");
				interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent);
				Vector3 interactedPos = interactedObject.transform.position;

				var destinationPoint = (GameObject)Instantiate (
					destionationPoint,
					new Vector3(interactedPos.x, 0.0f, interactedPos.z),
					destionationPoint.transform.rotation);
			} else {
				playerAgent.stoppingDistance = 0f;
				playerAgent.destination = interactionInfo.point;

				var destinationPoint = (GameObject)Instantiate (
					destionationPoint,
					interactionInfo.point,
					destionationPoint.transform.rotation);
			}
		}
	}

	void OnDestroy(){
		PlayerPrefs.SetFloat ("posX", gameObject.transform.position.x);
		PlayerPrefs.SetFloat ("posY", gameObject.transform.position.y);
		PlayerPrefs.SetFloat ("posZ", gameObject.transform.position.z);
		PlayerPrefs.SetFloat ("rotX", gameObject.transform.rotation.x);
		PlayerPrefs.SetFloat ("rotY", gameObject.transform.rotation.y);
		PlayerPrefs.SetFloat ("rotZ", gameObject.transform.rotation.z);
	}
}
