using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WorldInteraction : MonoBehaviour {
	NavMeshAgent playerAgent;

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
			if (interactedObject.tag == "interactable") {
				Debug.Log ("Interacted");
				interactedObject.GetComponent<Interactable> ().MoveToInteraction (playerAgent);
			} else {
				playerAgent.stoppingDistance = 0f;
				playerAgent.destination = interactionInfo.point;
			}
		}
	}
}
