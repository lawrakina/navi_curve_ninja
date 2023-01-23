using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UniRx;
using UnityEditor;
using UnityEngine;

public static class ParserGoogleTabAnalitics
{
   public static string ExcelPathOneTable = @"https://docs.google.com/spreadsheets/d/e/2PACX-1vT1rIHflKVgDXwxyYXfMNONr9f04Q90JoVTdVHGfnY0or8yRzqIiWC7OoArcoru6d3aLgxDY7ml64aF/pub?gid=0&single=true&output=csv";
   public static string ExcelPathOfferTable = @"https://docs.google.com/spreadsheets/d/e/2PACX-1vTFc_g9ajaCZU04t1PY7rjvIyWuCRRThaBM1gif-uKC_TEgiKjFgIbYktKQRBVWEfdbrm-djZgvEG15/pub?gid=0&single=true&output=csv";
   [MenuItem("GOOGLE/Download items data")]
   public static void DownloadOneTable()
   {
      WebClient client = new WebClient();

      string dnlad = client.DownloadString(ExcelPathOneTable);

      Debug.LogError(dnlad);
      Debug.LogError(Application.dataPath);
      var saveData = Application.dataPath.Replace("Assets", "GoogleData") + @"\ID_DATA.txt" ;
      
      if(File.Exists(saveData))File.Delete(saveData);
      File.WriteAllText(saveData, dnlad);
      var dataString = File.ReadAllLines(saveData);
      Debug.LogError(dataString.Length);
      var items = SOUtils.CreateSO<AnaliticsDataItems>();
      var header = dataString[0].Split(',');
      var codeIndex = header.ToList().IndexOf("Item_Code");
      var keyIndex = header.ToList().IndexOf("Item_Name");
      
      EditorUtility.SetDirty(items);
      int index = 0;
      Debug.LogError(items.Data.Count);
      foreach (var val in dataString)
      {
         if (index == 0)
         {
            Debug.LogError("Continue");
            index++;
            continue;
         }
         var splitter = val.Split(',');
         var dataItem = new Analitics.ItemsKeyCode.Container {Code = splitter[codeIndex], Key = splitter[keyIndex]};
         Debug.LogError("ADd " + splitter[0]);
         items.Data.Add(dataItem);

         
      }
      Debug.LogError(items.Data.Count);
      EditorUtility.SetDirty(items);
      SOUtils.SaveAsset(items, "Analytics_ID");
      Debug.Log("DOne");
      

   }
   
   
   [MenuItem("GOOGLE/Download Offers data")]
   public static void DownloadOfferTable()
   {
      WebClient client = new WebClient();

      string dnlad = client.DownloadString(ExcelPathOfferTable);

      Debug.LogError(dnlad);
      Debug.LogError(Application.dataPath);
      var saveData = Application.dataPath.Replace("Assets", "GoogleData") + @"\OFFER_DATA.txt" ;
      
      if(File.Exists(saveData))File.Delete(saveData);
      File.WriteAllText(saveData, dnlad);
      var dataString = File.ReadAllLines(saveData);
      Debug.LogError(dataString.Length);
      var items = SOUtils.CreateSO<AnaliticsSpecOffers>();
      var header = dataString[0].Split(',');
      var codeIndex = header.ToList().IndexOf("Special_Offer_Code");
      var keyIndex = header.ToList().IndexOf("Special_Offer_Name");
      var currentCode = header.ToList().IndexOf("Item_Code");
      var countCide = header.ToList().IndexOf("Item_Quantity");
      
      
      EditorUtility.SetDirty(items);
      int index = 0;
      Debug.LogError(items.Data.Count);
      foreach (var val in dataString)
      {
         if (index == 0)
         {
            Debug.LogError("Continue");
            index++;
            continue;
         }
         var splitter = val.Split(',');
         var dataItem = new Analitics.OfferKeyCode.OfferContainer {Special_Offer_Code = splitter[codeIndex], Special_Offer_Name = splitter[keyIndex], Item_Code = splitter[currentCode], Item_Quantity = splitter[countCide]};
         //Debug.LogError("ADd " + splitter[0]);
         items.Data.Add(dataItem);

         
      }
      Debug.LogError(items.Data.Count);
      EditorUtility.SetDirty(items);
      SOUtils.SaveAsset(items, "Analytics_ID_OFFER");
      Debug.Log("DOne");
      

   }
   
   
}
public static class SOUtils 
{
   public static T CreateSO<T>() where T : ScriptableObject
   {
      T asset = ScriptableObject.CreateInstance<T> ();
      return asset;
   }

   public static bool SaveAsset<T>(this T so, string nameSO) where T : ScriptableObject
   {
      var assetPathAndName = "Assets/Resources"+ "/" + nameSO + ".asset";
      // string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/ " + nameSO + ".asset");
      Debug.Log(assetPathAndName);
      Debug.LogError(assetPathAndName);
      try
      {
         /* if (File.Exists(Application.dataPath.Replace("Assets", assetPathAndName)))
          {
              File.Delete(Application.dataPath.Replace("Assets", assetPathAndName));
              AssetDatabase.Refresh();
          }*/
         AssetDatabase.CreateAsset (so, assetPathAndName);
         AssetDatabase.SaveAssets ();
         AssetDatabase.Refresh();
         EditorUtility.FocusProjectWindow ();
         Selection.activeObject = so;
      }
      catch (Exception e)
      {
         Debug.LogError(e);
         return false;
      }

      return true;
   }
}