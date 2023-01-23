using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class MobileDataUtils 
{
    public static string GetMac()
    {
        string mac = "";
        #if UNITY_EDITOR
        mac = GetMacAddress();
        
        
        
        #elif UNITY_ANDROID
        mac = GetAndroidMac();
        if (string.IsNullOrEmpty(mac)) mac = GetMacAddress();
#elif UNITY_IOS
mac = GetMacAddress();
#endif
        if (string.IsNullOrEmpty(mac)) mac = SystemInfo.deviceUniqueIdentifier;
        return mac;
    }

    private static string GetAndroidMac()
    {
        #if UNITY_ANDROID
        string id = "";
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaClass contextClass = new AndroidJavaClass("android.content.Context");
        string TELEPHONY_SERVICE = contextClass.GetStatic<string>("TELEPHONY_SERVICE");
        AndroidJavaObject telephonyService = activity.Call<AndroidJavaObject>("getSystemService", TELEPHONY_SERVICE);
        bool noPermission = false;
        if(id == "")
        {
            string mac = "00000000000000000000000000000000";
            try
            {
                StreamReader reader = new StreamReader("/sys/class/net/wlan0/address");
                mac = reader.ReadLine();
                reader.Close();

            }
            catch (Exception)
            {
                // ignored
            }

            id = mac.Replace(":", "");
            
            return mac;
        }

        if (id == null || id == "")
        {
            try
            {
                id = telephonyService.Call<string>("getDeviceId");
            }
            catch (Exception)
            {
                noPermission = true;
            }
        }

        if(id == null)
            id = "";
        // <= 4.5 : If there was a permission problem, we would not read Android ID
        // >= 4.6 : If we had permission, we would not read Android ID, even if null or "" was returned
        if((noPermission ) || (!noPermission && id == ""))
        {
            AndroidJavaClass settingsSecure = new AndroidJavaClass("android.provider.Settings$Secure");
            string ANDROID_ID = settingsSecure.GetStatic<string>("ANDROID_ID");
            AndroidJavaObject contentResolver = activity.Call<AndroidJavaObject>("getContentResolver");
            id = settingsSecure.CallStatic<string>("getString", contentResolver, ANDROID_ID);
            if(id == null)
                id = "";
        }
        if(id == "")
        {
            string mac = "00000000000000000000000000000000";
            try
            {
                StreamReader reader = new StreamReader("/sys/class/net/wlan0/address");
                mac = reader.ReadLine();
                reader.Close();
               
            }
            catch (Exception)
            {
                // ignored
            }
            return mac.Replace(":", "");;
        }
        return getMd5Hash(id);
        
        #else
        return "";
#endif
    }
    static string getMd5Hash(string input)
    {
        if (input == "")
            return "";
        #if UNITY_ANDROID
        MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
            sBuilder.Append(data[i].ToString("x2"));
        return sBuilder.ToString();
        #else
        return input;
#endif
    }
    
    static string GetMacAddress()
    {
        var reesult = "";
        foreach (NetworkInterface ninf in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (ninf.NetworkInterfaceType != NetworkInterfaceType.Ethernet) continue;
            reesult += ninf.GetPhysicalAddress().ToString();
            break;
            
        }
        return reesult;
    }
}
