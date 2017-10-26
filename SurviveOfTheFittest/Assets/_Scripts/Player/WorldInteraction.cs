using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class WorldInteraction : NetworkBehaviour {
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
		if (!isLocalPlayer) {
			return;
		}

		if (
			Input.GetMouseButtonDown (0) && 
			!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() &&
			!IsPointerOverUIObject()
			)
		{
			GetInteraction ();
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			//CmdFire();
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

/*	[Command]
	void CmdFire()
	{
		// This [Command] code is run on the server!

		// create the bullet object locally
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			transform.position + transform.forward,
			Quaternion.identity);

		bullet.GetComponent<Rigidbody>().velocity = transform.forward*4;

		// spawn the bullet on the clients
		NetworkServer.Spawn(bullet);

		// when the bullet is destroyed on the server it will automaticaly be destroyed on clients
		Destroy(bullet, 2.0f);
	}*/

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.red;
	}
}
