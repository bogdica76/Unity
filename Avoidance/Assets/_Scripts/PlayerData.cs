using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    private int PlayerDiamonds;
    private int PlayerLevel;
    private int adsCountDown;

    void Awake() {
        DontDestroyOnLoad(this);
    }
	// Use this for initialization
	void Start () {
		PlayerDiamonds = PlayerPrefs.GetInt ("diamonds");
        PlayerLevel = PlayerPrefs.GetInt("level");
        adsCountDown = PlayerPrefs.GetInt("ads");
		//Debug.Log (PlayerDiamonds);
        //Debug.Log(PlayerLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
        SaveData();
	}

    private void SaveData()
    {
        PlayerPrefs.SetInt("diamonds", PlayerDiamonds);
        PlayerPrefs.SetInt("level", PlayerLevel);
        PlayerPrefs.SetInt("ads", adsCountDown);        
    }

    public void IncreaseLevel() {
        PlayerLevel = PlayerLevel + 1;
    }

    public void RewardByTime(float aSeconds) {
        int seconds = Mathf.FloorToInt(aSeconds / 3);
        PlayerDiamonds = PlayerDiamonds + seconds;
        adsCountDown = adsCountDown + 1;
        if (adsCountDown == 3) {
            //dau reclama
            adsCountDown = 0;
            GameObject.Find("AdManager").GetComponent<AdManagerUnity>().ShowAd();
        }
        SaveData();
    }

    public void RewardVideo() {
        PlayerDiamonds += 50;
        SaveData();
    }

    public bool CanBuyUpgrade() {
        return PlayerDiamonds >= 250;
    }

    public void BuyUpgrade() {
        PlayerDiamonds -= 250;
        SaveData();
    }
}
