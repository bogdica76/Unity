using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuManagerScript : MonoBehaviour {

	public GameObject MainMenuPanel;
	public GameObject PowerUpsPanel;
    public GameObject ShopMenuPanel;
    

	public Text SpeedLevelText;
	public Text AccelerationLevelText;
	public Text ShieldLevelText;
    public Text DiamondsText;

	void Start () {
		MainMenuPanel.SetActive (true);
		PowerUpsPanel.SetActive (false);		
	}
	
	// Update is called once per frame
	void Update () {
        DiamondsText.text = PlayerPrefs.GetInt("diamonds").ToString();
    }

	public void PlayGame(){
		SceneManager.LoadScene ("Survival");
	}

	public void GoToPowerUps(){
		MainMenuPanel.SetActive (false);
		PowerUpsPanel.SetActive (true);
	}

    public void GoToShop() {
        MainMenuPanel.SetActive(false);
        ShopMenuPanel.SetActive(true);      
    }

	public void GoToMenu(){
		MainMenuPanel.SetActive (true);
		ShopMenuPanel.SetActive (false);	
	}

    public void GetDiamonds() {
        GameObject.Find("AdManager").GetComponent<AdManagerUnity>().ShowRewardedAd();
    }

    public void Quit() {
        //GameObject.Find("AdManager").GetComponent<GoogleAds>().ShowInterstitial();
        Application.Quit();
    }

    public void ShareFB() {
        string facebookshare = "https://www.facebook.com/sharer/sharer.php?u=" + Uri.EscapeUriString("https://play.google.com/store/apps/details?id=com.Acasa.Avoidance");
        Application.OpenURL(facebookshare);
    }
}
