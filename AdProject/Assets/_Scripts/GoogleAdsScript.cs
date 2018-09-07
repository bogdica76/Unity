using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;

public class GoogleAdsScript : MonoBehaviour {

    private BannerView bannerView;
    private RewardBasedVideoAd rewardBasedVideo;
    private InterstitialAd interstitial;
    private string appId = "ca-app-pub-8950130980707173~4993661178";
    private string bannerId = "ca-app-pub-8950130980707173/6506919368";
    private string rewardId = "ca-app-pub-8950130980707173/7433353182";
    private string interstitialId = "ca-app-pub-8950130980707173/1252848406";
    private double time = 0.0f;

    public Text texterr;
	// Use this for initialization
	void Start () {
        MobileAds.Initialize(appId);
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;


        RequestRewardBasedVideo();
        RequestInterstitial();
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time - time >= 30.0f) {
            RequestBanner();
        }
	}

    private void logErr(string aString) {
        texterr.text += aString;
    }

    private void RequestBanner() {
        time = Time.time;
        if (bannerView != null) bannerView.Destroy();

        bannerView = new BannerView(bannerId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    private void RequestInterstitial()
    {
        if (interstitial != null) interstitial.Destroy();

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(interstitialId);
        //========================================================
        // Called when an ad request has successfully loaded.
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    private void RequestRewardBasedVideo()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        rewardBasedVideo.LoadAd(request, rewardId);
    }

    public void ShowInterstitial()
    {
        logErr("Trying to show interstitial");
        if (interstitial.IsLoaded())
        {
            logErr("interstitial is showing");
            interstitial.Show();
        }
    }

    public void ShowRewardVideo()
    {
        logErr("Trying to show reward video");
        if (rewardBasedVideo.IsLoaded())
        {
            logErr("Reward video is displaying");
            rewardBasedVideo.Show();
        }
    }
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        logErr("Reward video was loaded");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        logErr(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        logErr("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        logErr("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        logErr("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        logErr(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        logErr("HandleRewardBasedVideoLeftApplication event received");
    }


    //=========================Interstitial
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //RequestInterstitial();
        logErr("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestInterstitial();
        logErr("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //RequestInterstitial();
        RequestRewardBasedVideo();
        logErr("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        logErr("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //RequestInterstitial();
        logErr("HandleAdLeavingApplication event received");
    }
}
