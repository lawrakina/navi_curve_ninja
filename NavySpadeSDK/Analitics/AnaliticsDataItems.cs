using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Analitics.ItemsKeyCode;
using UnityEngine;

[Serializable]
public class AnaliticsDataItems : ScriptableObject
{
    public List<Container> Data = new List<Container>();

    public Container GetContainer(string key)
    {
        var element = Data.FirstOrDefault(t => t.Code == key);
        return element;
    }
}

namespace Analitics.ItemsKeyCode
{
    [Serializable]
    public class Container 
    {
        public string Code;
        public string Key;
    }

}
