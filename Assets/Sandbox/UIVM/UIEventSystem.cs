using System;
using System.Collections;
using System.Collections.Generic;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using UnityEngine;

public static class UIEventSystem
{
    private const string UIEventKey = "UIVM_SWITCHER_";
    public static DisposeContainer AddEvent<T>(Enum key, Action<dynamic> value) => AddEvent<T>(key.ToString(), value);
    public static DisposeContainer AddEvent<T>(Enum key, Action<dynamic[]> value) => AddEvent<T>(key.ToString(), value);
    public static DisposeContainer AddEvent<T>(string key, Action<dynamic> value)
    {
        return DynamicEventManager.Add(UIEventKey +typeof(T)+"_" + key, value);
    }
    public static DisposeContainer AddEvent<T>(string key, Action<dynamic[]> value)
    {
        return DynamicEventManager.Add(UIEventKey +typeof(T)+"_" + key, value);
    }

    public static void InvokeEvent<T>(Enum key, dynamic value) => InvokeEvent<T>(key.ToString(), value);
    public static void InvokeEvent<T>(Enum key, params dynamic[] value) => InvokeEvent<T>(key.ToString(), value);
    public static void InvokeEvent<T>(string key, dynamic value)
    {
        DynamicEventManager.Invoke(UIEventKey +typeof(T)+"_" + key, value);
    }
    public static void InvokeEvent<T>(string key, params dynamic[] value)
    {
        DynamicEventManager.Invoke(UIEventKey +typeof(T)+"_" + key, value);
    }


    public static void RemoveEvent<T>(Enum key, Action<dynamic> value) => RemoveEvent<T>(key.ToString(), value);
    public static void RemoveEvent<T>(string key, Action<dynamic> value)
    {
        DynamicEventManager.Remove(UIEventKey +typeof(T)+"_" + key, value);
    }
    public static void RemoveEvent<T>(Enum key, Action<dynamic[]> value) => RemoveEvent<T>(key.ToString(), value);
    public static void RemoveEvent<T>(string key, Action<dynamic[]> value)
    {
        DynamicEventManager.Remove(UIEventKey +typeof(T)+"_" + key, value);
    }
}
