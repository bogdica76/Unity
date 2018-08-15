using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    private int PlayerDiamonds;
    private int PlayerLevel;

    void Awake() {
        DontDestroyOnLoad(this);
    }
	// Use this for initialization
	void Start () {
		PlayerDiamonds = PlayerPrefs.GetInt ("diamonds");
        PlayerLevel = PlayerPrefs.GetInt("level");
		Debug.Log (PlayerDiamonds);
        Debug.Log(PlayerLevel);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy(){
		PlayerPrefs.SetInt ("diamonds", PlayerDiamonds);
        PlayerPrefs.SetInt("level", PlayerLevel);
	}

    public void IncreaseLevel() {
        PlayerLevel = PlayerLevel + 1;
    }

    public void RewardByTime(float aSeconds) {
        int seconds = Mathf.FloorToInt(aSeconds);
        PlayerDiamonds = PlayerDiamonds + seconds;
    }
}
