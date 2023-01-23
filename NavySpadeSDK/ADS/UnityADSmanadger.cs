using System;
using System.Collections;
using System.Collections.Generic;
using AnalyticsEnums_;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

public class UnityADSmanadger : MonoBehaviour//, IUnityAdsListener
{
    public static UnityADSmanadger Instance;
    public static bool isStartShowAD = false;
    [SerializeField]
    private string GameIDAndroid = "";
    [SerializeField]
    private string GameIDiOS = "";

    public string ADSPlacementRewardedID;

    public string GetCurrentADSID => Application.platform == RuntimePlatform.Android ? GameIDAndroid : GameIDiOS;

    public bool isTestMode = false;
    public bool isInitialize = false;

    void Awake()
    {
        if (Instance == null)
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
       /* Advertisement.Initialize(GetCurrentADSID, isTestMode);
        isInitialize = true;
        Advertisement.AddListener(this);*/
    }

    public Action rewardAction = null;
    public void ShowRewardADS(Action endAction)
    {
        /*AnalyticsBridge.SendCustomEventsOther("Ads:Unity",1);
        rewardAction = endAction;
        isStartShowAD = true;        
        Advertisement.Show (ADSPlacementRewardedID);*/
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        
    }

    public void OnUnityAdsDidError(string message)
    {
        isStartShowAD = false;
       
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        
    }

    /*public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        isStartShowAD = false;
        if (showResult == ShowResult.Finished)
        {
            rewardAction?.Invoke();
            DayMissionController.Instance?.EventSystem.InvokeWatchAD(1);
        }

        
    }*/
}
