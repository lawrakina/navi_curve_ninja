using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class DeviceInfo : MonoBehaviour
{
    public static DeviceInfoParams data = new DeviceInfoParams();

}

public class DeviceInfoParams
{
    public  int sessionNumber;

    public string AppVersion
    {
        get { return Application.version; }
    }

    public  DeviceInfoParams()
    {
        PlayerPrefs.SetInt("SessionNumber_local_data", PlayerPrefs.GetInt("SessionNumber_local_data", 0) +1);
        sessionNumber = PlayerPrefs.GetInt("SessionNumber_local_data", 0);
    }
}