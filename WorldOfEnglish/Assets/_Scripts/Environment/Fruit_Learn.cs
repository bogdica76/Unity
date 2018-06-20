using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Fruit_Learn : Interactable
{
	public string textToDisplay;
	public Text textComponent;
	
	public override void Interact ()
	{
		try {
			gameObject.GetComponent<AudioSource> ().Play ();
			textComponent.text = textToDisplay;
			StartCoroutine(ClearText());
		} catch (Exception e) {
			Debug.LogException (e, this);
		}

		
	}

	IEnumerator ClearText(){
		yield return new WaitForSeconds(2);
		textComponent.text = "";
	}
}
