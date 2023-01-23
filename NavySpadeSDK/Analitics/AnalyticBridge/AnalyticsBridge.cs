using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public static class AnalyticsBridge
{
   public static bool EnableGameAnalytics = true;
   public static bool EnableUnityAnalytics = true;
   public static void SendEvent(string id, string data)
   {
      var resutl = Analytics.CustomEvent(id, new Dictionary<string, object>
      {
         {"Data", data}
      });
   }

   public static void SendBuyForRealEvent(string currency_s, int amount_i, string itemType_s, string itemId_s, string cartType_s, string receipt_s, string signature_s)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.BuyEventSend(currency_s, amount_i, itemType_s, itemId_s, cartType_s, receipt_s, signature_s);
      }
   }
   public static void SendBuyForRealEventIOS(string currency_s, int amount_i, string itemType_s, string itemId_s, string cartType_s, string receipt_s)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.BuyEventSendIOS(currency_s, amount_i, itemType_s, itemId_s, cartType_s, receipt_s);
      }
   }

   public static void SendSpend(string currency_s, float amount_f, string itemType_s, string itemId_s)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.SendResourcesEventSpend(currency_s, amount_f, itemType_s, itemId_s);
      }
   } 
   
   public static void SendBuy(string currency_s, float amount_f, string itemType_s, string itemId_s)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.SendResourcesEventAdd(currency_s, amount_f, itemType_s, itemId_s);
      }
   }

   public static void SendStartLevel(string pg1, string pg2, string pg3, int score)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.ProgressionEventStart(pg1, pg2,pg3, score);
      }
   }

   public static void SendCompleteLevel(string pg1, string pg2,  int score)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.ProgressionEventStart(pg1, pg2, score);
      }
   }
   public static void SendCompleteLevel(string pg1, string pg2, string pg3,  int score)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.ProgressionEventStart(pg1, pg2, pg3, score);
      }
   }
   
   public static void SendFailLevel(string pg1, string pg2, string pg3, int score)
   {
      if (EnableGameAnalytics)
      {
         GATransmitter.ProgressionEventFail(pg1, pg2,pg3, score);
      }
   }

   public static void SendCustomEventsOther(string key, float value)
   {
      GATransmitter.SendCustomEvent(key, value);
   }

}
