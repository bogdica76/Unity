using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int PlayerDiamonds = PlayerPrefs.GetInt ("diamonds");
		Debug.Log (PlayerDiamonds);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		PlayerPrefs.SetInt ("diamonds", 100);
	}
}
