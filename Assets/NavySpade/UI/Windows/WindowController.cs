using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    public static WindowController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<WindowObject> windows = new List<WindowObject>();

    public void OpenWindow<T>(string name, Action<T> openAction, Action closeAction) where T : WindowObject
    {
        var window = windows.FirstOrDefault(e => e.gameObject.name == name);
        window.Activete(openAction, closeAction);
    }
}
