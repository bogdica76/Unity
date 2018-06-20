using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigGameManager : MonoBehaviour {

	private int numberOfBullets;
	public GameObject panouInformatii;

	void Start () {
		numberOfBullets = 5;
		setAmmoQuantity ();
	}

	public void collectBullets (){
		numberOfBullets = numberOfBullets + 5;
		setAmmoQuantity ();
	}

	public bool checkForAmmo(string ammoType){
		if (ammoType == "bullets" && numberOfBullets > 0) {
			return true;
		}

		return false;	
	}

	public void showInfo(string info){
		panouInformatii.SetActive (true);
		panouInformatii.GetComponentInChildren<Text> ().text = info;
	}

	public void hideInfo(){
		GameObject.Find ("PanelInfo").SetActive (false);
	}

	public void setAmmoQuantity(){
//		GameObject.Find ("cantitateMunitie").GetComponent<Text>().text = numberOfBullets.ToString ();
	}

	public void shootAmmo(string ammoType){
		if (ammoType == "bullets") {
			numberOfBullets = numberOfBullets - 1;
			setAmmoQuantity ();
		}
	}

	// Use this for initialization

	
	// Update is called once per frame
	void Update () {
		
	}



}
