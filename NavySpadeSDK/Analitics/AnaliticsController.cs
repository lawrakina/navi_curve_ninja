using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class AnaliticsController 
{
    public static bool InternetConnect;
    public static AnaliticsController Instance;

    
    public static CompositeDisposable disp = new CompositeDisposable();
    public static bool isInitialize = false;

    

    


    //other not connected
    public static void LicenceLookAt()
    {
    
#if LEVEL_EDITOR
       // return;
#endif
       /* var container = new NewAnalyticsContainer("FL_-4", "ConditionsLookedAt", "", "1", "N");
        container.AddField("ConditionsLookedAt");
        container.AddField("Empty");
        container.SendEvent();*/
    }
    public static void LicenceAccepted()
    {
    
#if LEVEL_EDITOR
        //return;
#endif
       /* var container = new NewAnalyticsContainer("FL_-3", "ConditionsAccepted", "", "1", "N");
        container.AddField("ConditionsAccepted");
        container.AddField("Empty");
        container.SendEvent();*/
    }
    public static void TutorialChoose(int isExpert)
    {
    
#if LEVEL_EDITOR
       // return;
#endif
      /*  var container = new NewAnalyticsContainer( "TutorialChoice", "", "1", "N");
        container.AddField("TutorialChoice");
        var stringFormated = isExpert.ToString();
        container.AddField(stringFormated);
        container.SendEvent();*/
    }
    

    public static void GameLike(int x)
    {
#if LEVEL_EDITOR
      //  return;
#endif
      /*  string id = x < 0 ? "Game_Like" : ("Game_Like_" + x);
        var container = new NewAnalyticsContainer( id, "", "", "N");
        
        container.AddField(id);
        container.SendEvent();*/
    }
    public static void GameNotLike(int x)
    {
#if LEVEL_EDITOR
       // return;
#endif
     /*   string id = x < 0 ? "Game_Not_Like" : ("Game_Not_Like" + x);
        var container = new NewAnalyticsContainer( id, "", "", "N");
        
        container.AddField(id);
        container.SendEvent();*/
    }
    
    public static void GameOnboarding(int x)
    {
#if LEVEL_EDITOR
       // return;
#endif
       /* string id = "Onboarding";
        var container = new NewAnalyticsContainer( id, "", "", "N");
        
        container.AddField(id);
        container.AddField(x.ToString());
        container.SendEvent();
        AnalyticsBridge.SendCustomEventsOther(id, x);*/
    }

    
//need connect level events
    
    

//Completed
    public static void LevelStart(string preactivatedBoosters, int attempt, DateTime startTime)
    {
    
        //#if LEVEL_EDITOR
        return;
       // #endif
        /*var container = new NewAnalyticsContainer("LevelStart", "", GlobalParameters.LevelNumber.ToString(),"");
        container.AddField("LevelStart");
        var str = "";
        str += attempt;
        str += "[0";
        str += "[" + (DateTime.Now - startTime).TotalMilliseconds;
        str += "[0";
        container.AddField(str);
        container.SendEvent();*/
        //AnalyticsBridge.SendStartLevel("MAIN", "LEVEL_" + GlobalParameters.LevelNumber, "LEVEL_" + GlobalParameters.LevelNumber + ( "_GAME"), 0);
    }

    //connected
    public static void LevelWin( int attemptCount, int coinsCollected,   DateTime startGame)
    {

        

    
        //#if LEVEL_EDITOR
       // return;
        //#endif
        /*var container = new NewAnalyticsContainer("LevelWin", "", GlobalParameters.LevelNumber.ToString(), "N");
        
        
        container.AddField("LevelWin");
        var sf = "";
        sf += attemptCount.ToString();
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + coinsCollected;
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + "0";
        
        sf += "]" + (DateTime.Now - startGame).TotalMilliseconds;
        container.AddField(sf);
        container.SendEvent();
        
        AnalyticsBridge.SendCompleteLevel("MAIN", "LEVEL_" + GlobalParameters.LevelNumber, "LEVEL_" + GlobalParameters.LevelNumber + ( "_GAME"), 0);
    */
    }
    

    public static void LevelQuit( string preactivatedBoosters, string ActivatedBooster, int scoreInLevel)
    {
    
//#if LEVEL_EDITOR
       // return;
//#endif
        
       // AnalyticsBridge.SendFailLevel("", "", "" + ( "_GAME"), scoreInLevel);
        //AnalyticsBridge.SendFailLevel(info.Number, AnalyticsEnums.GetLevelKeyAtInfo(info), info.Uproshenie, container.JobString);
    }

    public static void ProgressEvent(string s1, string s2, int attempt)
    {
      //  return;
       // AnalyticsBridge.SendCompleteLevel(s1, s2,  attempt);
    }

    public static int AtemptCount(int number)
    {
        return PlayerPrefs.GetInt("LevelCompCount_" + number, 0);
    }

    public static void SetAtemptCount(int number, int count)
    {
        PlayerPrefs.SetInt("LevelCompCount_" + number, count);
    }
    
    
    //Connected
    public static void LevelFail( int attemptCount,  string coins_collected,   DateTime timeToEndGame)
    {
        return;
       /* var container = new NewAnalyticsContainer("LevelFail", "", GlobalParameters.LevelNumber.ToString(), AnalyticsEnums.GetLevelKeyAtInfo());
        container.AddField("LevelFail");
        var sf = "";
        sf += attemptCount.ToString();
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + coins_collected;
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + "0";
        
        sf += "]" + (DateTime.Now - timeToEndGame).TotalMilliseconds;
        sf += "]" + "0";
        sf += "]" + "0";
        sf += "]" + "0";
        container.AddField(sf);
        container.SendEvent();

        AnalyticsBridge.SendFailLevel("MAIN", "LEVEL_" + GlobalParameters.LevelNumber, "LEVEL_" + GlobalParameters.LevelNumber + ( "_GAME"), 0);*/
    }
    
    
    
    public static void LevelThrow( string preactivatedBoosters, string ActivatedBooster, int bubbleInHand, string ruby_collected, int bubbleAdded, int scoreInLevel, int shotCount, double timeToThrow, double tapsCount, int countInGrid, int groupCount, int goalsLeft, float x, float y )//, double timeToEndGame, int countInGrid, int groupCount, int goalsLeft)
    {
      //  return;
#if LEVEL_EDITOR
       // return;
#endif
       /* if (AnalyticCounter.GetCount() >= 450) return;
        var container = new NewAnalyticsContainer("LevelThrow", "", "","");
       
        container.SendEvent();*/
    }

    //need connect app events
    public static void Initialize()
    {

        if (isInitialize) return;
      /*  var analCounter = MainHandler.Instance.gameObject.AddComponent<AnalyticCounter>();
        analCounter.Initialize();
        if(disp == null || disp.IsDisposed) disp = new CompositeDisposable();
        MainHandler.Instance.OnDestroyAsObservable().Subscribe(t =>
        {
            CloseApp();
            isInitialize = false;
            disp.Dispose();
        }).AddTo(disp);
        */
        isInitialize = true;
        
        
        StartApp();
    }
    
    //connected
    public static void FirstLaunch()
    {
        //return;
#if LEVEL_EDITOR
       // return;
#endif
       /* var container = new NewAnalyticsContainer("FL_-5", "FirstLaunch", "", "1", "N");
        container.AddField("FirstLaunch");
        container.AddField("Empty");
        container.SendEvent();*/
    }
    
    //connected
    public static void DeviceInformation()
    {
   // return;
#if LEVEL_EDITOR
      //  return;
#endif
      /*  var container = new NewAnalyticsContainer( "DeviceInfo", "", "", "N");
        container.AddField("DeviceInfo");
        var stringFormated = "";
        stringFormated += SystemInfo.operatingSystem;
        stringFormated += "]" + SystemInfo.deviceType;
        stringFormated += "]" + SystemInfo.deviceModel;
        stringFormated += "]" + SystemInfo.systemMemorySize;
        stringFormated += "]" + (SystemInfo.processorFrequency / 1000f).ToString("F2");
        stringFormated += "]" + Screen.width + "x" + Screen.height;
        stringFormated += "]" + (1);
        container.AddField(stringFormated);
        container.SendEvent();*/
    }

    private static DateTime startTime;
    
    //connected
    public static void StartApp()
    {
        
        startTime = DateTime.Now;
       /* return;
        var container = new NewAnalyticsContainer( "OpenApp","" , GlobalParameters.LevelNumber.ToString(), "N");
        container.AddField("OpenApp");
        
        var sf = "";
        sf += "-1";
        sf += "]0" ;
        sf += "]0" ;
        sf += "]0" ;
        sf += "]" + Coins.CoinsCount.ToString();// LevelInfoSaver.GetSingleton().Coins.GetCoins();
        sf += "]0" ;
        sf += "]0" ;
        sf += "]" ;
        sf += "]" ;
        container.AddField(sf);
        
        container.SendEvent();*/
    }

    public static void ResumeApp()
    {
    
#if LEVEL_EDITOR
        //return;
#endif
       /* var container = new NewAnalyticsContainer( "ResumeApp", "", GlobalParameters.LevelNumber.ToString(), "N");
        container.SendEvent();*/
    }
    public static void PauseApp()
    {
#if LEVEL_EDITOR
       // return;
#endif
        /*var container = new NewAnalyticsContainer( "PauseApp", "", GlobalParameters.LevelNumber.ToString(), "N");
        
        container.SendEvent();*/
    }

    public static bool sendCloseApp = false;
    
    //connected
    public static void CloseApp()
    {
       // return;
#if LEVEL_EDITOR
      //  return;
#endif
       /* if (sendCloseApp) return;
        sendCloseApp = true;
        var container = new NewAnalyticsContainer( "CloseApp", "", GlobalParameters.LevelNumber.ToString(), "N");
        container.AddField("CloseApp");
        
        var sf = "";
        sf += "-1";
        sf += "]0" ;
        sf += "]0" ;
        sf += "]0" ;
        sf += "]" + Coins.CoinsCount.ToString();// LevelInfoSaver.GetSingleton().Coins.GetCoins();
        sf += "]0" ;
        sf += "]0" ;
        var time_ = DateTime.Now - startTime; 
        sf += "]" + time_.TotalSeconds + "" + time_.Milliseconds.ToString("000") ;
        sf += "]" ;
        container.AddField(sf);
        container.SendEvent();*/
    }

    //TODO: Resurrection
    public static void ResurrectionEvent()
    {
        //return;
       /* AnalyticsBridge.SendBuy(AnalyticCurrency.Resurrection.ToString(), MainHandler.Instance.Config.keysCountADS, AnalyticItemType.RewardedAds.ToString(),AnalyticItemId.Resurrection.ToString() );
        AnaliticsController.Reward_Ads_ItemID("4",  "1" + "Resurrection");*/
    }
    
    //not connected
    public static void BuyForReal_ItemID(string itemID, string formatedData)
    {
#if LEVEL_EDITOR
        //return;
#endif
       /* var id = "BuyForReal_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", "", "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }
    public static void Reward_Ads_ItemID(string itemID, string formatedData)
    {
       // return;
        #if LEVEL_EDITOR
       // return;
        #endif
        /*var id = "Reward_Ads_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", GlobalParameters.LevelNumber.ToString(), "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }
    public static void Reward_FromGame_ItemID(string itemID, string formatedData)
    {
#if LEVEL_EDITOR
       // return;
#endif
        /*var id = "Reward_FromGame_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", "", "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }
    public static void Reward_Daily_X(string itemID, string formatedData)
    {
#if LEVEL_EDITOR
       // return;
#endif
       /* var id = "Reward_Daily_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", "", "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }
    public static void Reward_FortuneWheel_ItemID_XXX(string itemID, string formatedData)
    {
#if LEVEL_EDITOR
       // return;
#endif
        /*var id = "Reward_FortuneWheel_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", "", "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }
    
    public static void Reward_RQuest_ItemID_XXX(string itemID, string formatedData)
    {
#if LEVEL_EDITOR
        //return;
#endif
        /*var id = "Reward_RQuest_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", "", "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }
    
    public static void Purchase_ItemID(string itemID, string formatedData)
    {
        #if LEVEL_EDITOR
       //return;
        #endif
       /* var id = "Purchase_ItemID_" + itemID;
        var container = new NewAnalyticsContainer( id, "", "", "N");
        container.AddField(id);
        container.AddField(formatedData);
        container.SendEvent();*/
    }


    public static void SendAnalyticsEditorEvent()
    {
       /* var container = new NewAnalyticsContainer( "Pets_EDITOR", "", "", "N");
        container.AddField("Pets_EDITOR");
        container.AddField(DeviceInfo.data.AppVersion);
        container.SendEvent();*/
    }
    public static void SendAnalyticsEditorStartLevelEvent()
    {
        /*var container = new NewAnalyticsContainer( "Pets_EDITOR_start_level", "", "", "N");
        container.AddField("Pets_EDITOR_start_level");
        container.AddField(DeviceInfo.data.AppVersion);
        container.SendEvent();*/
    }


    public static void SendChangeTask(string id, string hard, string progressLoss)
    {
       /* var id_ = "Switch_" + id;
        var container = new NewAnalyticsContainer( id_, "", "", "N");
        container.AddField(id_);
        var str = "";
        //str = id;
        str +=  hard;
        str += "]" + 0;
        str += "]" + progressLoss;
        container.AddField(str);
        container.SendEvent();*/
    }
    public static void SendTaskComplete(string id, string hard)
    {
        /*var id_ = "Complete_" + id;
        var container = new NewAnalyticsContainer( id_, "", "", "N");
        container.AddField(id_);
        var str = "";
        //str = id;
        str +=  hard;
        str += "]" + 0;
        container.AddField(str);
        
        container.SendEvent();*/
    }
    
}

/*
public class AnaliticsBuster
{
    public string Name;
    public int BusterCount;
}*/