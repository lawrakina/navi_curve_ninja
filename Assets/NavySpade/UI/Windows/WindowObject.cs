using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowObject : MonoBehaviour
{
    public GameObject body;
    private Action closeAction;
    public virtual void Activete<T>(Action<T> endActiveAction, Action closeAction_) where T: WindowObject
    {
        
        closeAction = closeAction_;
        body.SetActive(true);
        endActiveAction?.Invoke(this as T);
    }

    public void Deactivate()
    {
        closeAction?.Invoke();
        body.SetActive(false);
    }
}
