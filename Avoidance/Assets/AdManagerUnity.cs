using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManagerUnity : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Start() {
        Advertisement.Initialize("2744884");
    }
	// Update is called once per frame
	void Update () {
	
	}

    public void ShowAd() {
        if (Advertisement.IsReady("video"))
        {
            Advertisement.Show("video");
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                GameObject.Find("PlayerManager").GetComponent<PlayerData>().RewardByTime(100);
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }


}
