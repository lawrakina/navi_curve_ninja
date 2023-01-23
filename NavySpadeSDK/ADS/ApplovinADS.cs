using System;
using System.Collections;
using System.Collections.Generic;
using ADS;
using UnityEngine;

public class ApplovinADS : MonoBehaviour
{
    public static ApplovinADS Instance;
    public string Initializekey = "fABWFrXJIEfOUi3iA7lK2FGoaXRQge4ovgxuaiZvoDD4sFS5QAorHVa72eGKz8gEuZB4dew7TBGUtoP7-78gai";
    void Awake()
    {
        if (Instance == null || Instance == this)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Initialize();

        }
        else
        {
            Destroy(gameObject);
        }
    }
    

    public void Initialize()
    {
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            InitializeRewardedAds();
            //InitializeBannerAds();
        };
        MaxSdk.SetSdkKey("fABWFrXJIEfOUi3iA7lK2FGoaXRQge4ovgxuaiZvoDD4sFS5QAorHVa72eGKz8gEuZB4dew7TBGUtoP7-78gai");
        MaxSdk.InitializeSdk();
    }

    public string bannerAdUnitId = "YOUR_BANNER_AD_UNIT_ID";
    public string adUnitId = "YOUR_AD_UNIT_ID";
    
    public string bannerAdUnitIdIOS = "YOUR_BANNER_AD_UNIT_ID";
    public string adUnitIdIOS = "YOUR_AD_UNIT_ID";

    public string GetKeyBanner()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) return bannerAdUnitIdIOS;
        else return bannerAdUnitId;
    }
    public string GetAdUnitId()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer) return adUnitIdIOS;
        else return adUnitId;
    }
    int retryAttempt;

    private Action endShowReward = null;

    public bool ShowRewardADS(Action endAction)
    {
        if (MaxSdk.IsRewardedAdReady(GetAdUnitId()))
        {
            var info = MaxSdk.GetAdInfo(GetAdUnitId());
            endShowReward = endAction;
            MaxSdk.ShowRewardedAd(GetAdUnitId());
            AnalyticsBridge.SendCustomEventsOther("Ads:AppLovin",1);
            return true;
        }

        return false;
    }

    public bool isReadyBanner = false;
    
    public void InitializeBannerAds()
    {
        if (GlobalADS.IsCompletePurchase)
        {
            isReadyBanner = false;
            return;
            
        }
        // Banners are automatically sized to 320x50 on phones and 728x90 on tablets
        // You may use the utility method `MaxSdkUtils.isTablet()` to help with view sizing adjustments
        MaxSdk.CreateBanner(GetKeyBanner(), MaxSdkBase.BannerPosition.BottomCenter);
        var screenDensity = MaxSdkUtils.GetScreenDensity();
        //var dpBanner = Screen.width / screenDensity;
        //MaxSdk.SetBannerWidth(bannerAdUnitId, dpBanner);
        // Set background or background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(GetKeyBanner(), new Color(0,0,0,0));
        
        isReadyBanner = true;
    }

   /* public void ShowBanner()
    {
        MaxSdk.ShowBanner(GetKeyBanner());
    }

    public void HideBanner()
    {
        MaxSdk.HideBanner(GetKeyBanner());
    }*/
    
    private void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first rewarded ad
        LoadRewardedAd();
    }

    private void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(GetAdUnitId());
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(adUnitId) will now return 'true'

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));
    
        Invoke("LoadRewardedAd", (float) retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId) {}

    private void OnRewardedAdClickedEvent(string adUnitId) {}

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        endShowReward?.Invoke();
        endShowReward = null;
    }
}
