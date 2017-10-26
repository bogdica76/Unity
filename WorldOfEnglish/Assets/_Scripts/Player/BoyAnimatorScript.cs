using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyAnimatorScript : MonoBehaviour {

	private Vector3 prevPos;
	public Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		prevPos = transform.position;
	}

	// Update is called once per frame
	void Update () {		
		if (prevPos != transform.position) {
			anim.SetBool ("Moving", true);
			anim.SetBool ("Waiting", false); 
			//anim.Play ("Run");
		} else {
			anim.SetBool ("Moving", false);
			anim.SetBool ("Waiting", true); 
		}

		prevPos = transform.position;
	}
}
