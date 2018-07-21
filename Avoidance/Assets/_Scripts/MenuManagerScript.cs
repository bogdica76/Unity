using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour {

	public GameObject MainMenuPanel;
	public GameObject PowerUpsPanel;

	public Text SpeedLevelText;
	public Text AccelerationLevelText;
	public Text ShieldLevelText;

	void Start () {
		MainMenuPanel.SetActive (true);
		PowerUpsPanel.SetActive (false);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayGame(){
		SceneManager.LoadScene ("TestLevel");
	}

	public void GoToPowerUps(){
		MainMenuPanel.SetActive (false);
		PowerUpsPanel.SetActive (true);
	}

	public void GoToMenu(){
		MainMenuPanel.SetActive (true);
		PowerUpsPanel.SetActive (false);		
	}
}
