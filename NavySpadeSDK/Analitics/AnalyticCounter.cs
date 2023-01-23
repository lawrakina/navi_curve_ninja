using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticCounter : MonoBehaviour
{
    public static AnalyticCounter Instance;
    public void Initialize()
    {
        Instance = this;
        StartCoroutine(coroChechDataEvents());
    }

    IEnumerator coroChechDataEvents()
    {
        while (true)
        {
            var now = DateTime.Now;
            if(containers_ACC.Count>0) containers_ACC.RemoveAll(t => t.time < now.AddHours(-1));
            yield return null;
        }
    }
    public List<AnalyticsCounterContainer> containers_ACC = new List<AnalyticsCounterContainer>();
   
    public static void AddEvent()
    {
        if (Instance == null) return;
        Instance.containers_ACC.Add(new AnalyticsCounterContainer(){time = DateTime.Now});
    }
    
    
    
    public static int GetCount()
    {
        if (Instance == null) return 0;
        int value = 0;
        value = Instance.containers_ACC.Count;
        return value;
    }
}

[Serializable]
public class AnalyticsCounterContainer
{
    public DateTime time;
}