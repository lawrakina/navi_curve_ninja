using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using EventSystem.Runtime.Core.Dispose;
using NavySpade.Modules.Extensions.UnityTypes;
using Toolkit.Extensions.UnityTypes;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUI<T> : ExtendedMonoBehavior
{
    [SerializeField] private string ID;
    public T Element; 
    
    public virtual void Awake()
    {
        /*AddEvent("SetActive_" + ID, value =>
        {
            if(value is bool) gameObject.SetActive(value);
        });*/
        Initialize();
    }
    public abstract void Initialize();
    public EventDisposal Disposal = new EventDisposal();

   /* public void AddEvent(Enum key, Action<dynamic> value) => AddEvent(key.ToString(), value);
    public void AddEvent(Enum key, Action<dynamic[]> value) => AddEvent(key.ToString(), value);
    public void AddEvent(string key, Action<dynamic> value)
    {
        UIEventSystem.AddEvent<T>(key, value).AddTo(Disposal);
    }
    public void AddEvent(string key, Action<dynamic[]> value)
    {
        UIEventSystem.AddEvent<T>(key, value).AddTo(Disposal);
    }
    public void InvokeEvent(Enum key, dynamic value) => InvokeEvent(key.ToString(), value);
    public void InvokeEvent(Enum key, dynamic[] value) => InvokeEvent(key.ToString(), value);
    public void InvokeEvent(string key, dynamic value)
    {
        UIEventSystem.InvokeEvent<T>(key, value);
    }
    public void InvokeEvent(string key, dynamic[] value)
    {
        UIEventSystem.InvokeEvent<T>(key, value);
    }*/
    public virtual void OnDestroy()
    {
        Disposal.Dispose();
    }

}

