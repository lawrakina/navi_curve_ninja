using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class NewAnalyticsContainer
{
   public Dictionary<string, object> data = new Dictionary<string, object>();
   public string name;
   public string JobString;
   
   public void AddField(string value)
   {
       JobString += "[" + value;
   }

   public void SendEvent()
   {
       if (AnalyticCounter.GetCount() >= 500) return;
       AnalyticsBridge.SendEvent(name, JobString);
       /*var resutl = Analytics.CustomEvent(name, new Dictionary<string, object>
       {
           {"Data", JobString}
       });*/
       
       AnalyticCounter.AddEvent();
       Analytics.FlushEvents();
   }
   
   public NewAnalyticsContainer(string name_, string LevelVersion, string LevelNumber = null, string LevelKey  = "N")
   {
      name = name_;
      if (string.IsNullOrEmpty(LevelNumber)) LevelNumber = AnalyticsEnums.GetActualLevelNumber.ToString();
     
      var stringData = "";
      var macdata = SystemInfo.deviceUniqueIdentifier + "_3";
      stringData += macdata + "[";
      stringData += macdata + "_" + DeviceInfo.data.sessionNumber + "_" + DeviceInfo.data.AppVersion + "[";
      if (string.IsNullOrEmpty(LevelVersion)) LevelVersion = "1.1";
      stringData += LevelKey + "_" + LevelNumber + "_V." + LevelVersion + "." + (1) + "["; 
      stringData += ScreenStackHandler.GetInstance.FormateData();
      JobString = stringData;
   }
   public NewAnalyticsContainer(string ID_LEVEL, string name_, string LevelVersion, string LevelNumber , string LevelKey = "N")
   { 
      name = name_;
      if (string.IsNullOrEmpty(LevelNumber)) LevelNumber = AnalyticsEnums.GetActualLevelNumber.ToString();
      var stringData = "";
      var macdata = SystemInfo.deviceUniqueIdentifier + "_3";
      stringData += macdata + "[";
      stringData += macdata + "_" + DeviceInfo.data.sessionNumber + "_" + DeviceInfo.data.AppVersion + "[";
      stringData +=     ID_LEVEL + "["; 
      stringData += ScreenStackHandler.GetInstance.FormateData();
      JobString = stringData;
   }

   
}