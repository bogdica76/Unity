using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPreview : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PreviewShip(int aShipIndex) {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        transform.GetChild(aShipIndex).gameObject.SetActive(true);
    }
}
