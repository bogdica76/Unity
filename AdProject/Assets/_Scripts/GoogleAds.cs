using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class GoogleAds : MonoBehaviour {

    private InterstitialAd interstitial;
    private bool isShowing = false;
    // Use this for initialization
    void Start () {
        string appId = "ca-app-pub-8950130980707173~4993661178";
        MobileAds.Initialize(appId);
        RequestInterstitial();
    }
	
	// Update is called once per frame
	void Update () {
        if (isShowing) return;
        ShowInterstitial();
	}

    private void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-8950130980707173/1252848406";

        // Clean up interstitial ad before creating a new one.
        if (interstitial != null)
        {
            interstitial.Destroy();
        }

        // Create an interstitial.
        interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        //this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        //this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        //this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        //this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        //this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Load an interstitial ad.
        interstitial.LoadAd(this.CreateAdRequest());
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            //.AddTestDevice(AdRequest.TestDeviceSimulator)
            //.AddTestDevice("0123456789ABCDEF0123456789ABCDEF")
            //.AddKeyword("game")
            //.SetGender(Gender.Male)
            //.SetBirthday(new DateTime(1985, 1, 1))
            //.TagForChildDirectedTreatment(false)
            //.AddExtra("color_bg", "9B30FF")
            .Build();
    }

    public void ShowInterstitial()
    {
       

        if (interstitial.IsLoaded() && !isShowing)
        {
            isShowing = true;
            interstitial.Show();
            //this.interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial is not ready yet");
        }
    }

}
