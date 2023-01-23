using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public static class GATransmitter 
{
    public static void BuyEventSend(string currency_s, int amount_i, string itemType_s, string itemId_s, string cartType_s, string receipt_s, string signature_s)
    {
        #if UNITY_ANDROID
        GameAnalytics.NewBusinessEventGooglePlay(currency_s, amount_i, itemType_s, itemId_s, cartType_s, receipt_s, signature_s);
        #else
        GameAnalytics.NewBusinessEvent(currency_s, amount_i, cartType_s, itemId_s, cartType_s);
        #endif
    }

    public static void BuyEventSendIOS(string currency_s, int amount_i, string itemType_s, string itemId_s,string cartType_s, string receipt_s)
    {
        #if UNITY_IPHONE || UNITY_IOS
        GameAnalytics.NewBusinessEventIOS(currency_s, amount_i, itemType_s, itemId_s, cartType_s, receipt_s);
        #endif
        
    }

    public static void SendResourcesEventSpend(string currency_s, float amount_f, string itemType_s, string itemId_s)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, currency_s, amount_f, itemType_s, itemId_s);
    }
    public static void SendResourcesEventAdd(string currency_s, float amount_f, string itemType_s, string itemId_s)
    {
        GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, currency_s, amount_f, itemType_s, itemId_s);
    }

    public static void ProgressionEventStart(string pg1, string pg2, string pg3, int score)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, pg1, pg2, pg3, score);
    }
    public static void ProgressionEventStart(string pg1, string pg2,  int score)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, pg1, pg2,  score);
    }
    public static void ProgressionEventComplete(string pg1, string pg2, string pg3, int score)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, pg1, pg2, pg3, score);
    }
    public static void ProgressionEventFail(string pg1, string pg2, string pg3, int score)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, pg1, pg2, pg3, score);
    }
    public static void SendCustomEvent(string key)
    {
        GameAnalytics.NewDesignEvent(key);
    }
    
    public static void SendCustomEvent(string key, float value)
    {
        GameAnalytics.NewDesignEvent(key,value);
    }

    
}
