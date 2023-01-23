using System.Collections;
using System.Collections.Generic;
using NavySpade.Modules.Utils.ParametersSetter.Attributes;
using NavySpade.Modules.Utils.ParametersSetter.Editor;
using UnityEngine;

public static class ComponentStorageParameters
{
    [SetSettings]
    private static void SetDefine()
    {
        DefineSetter.SetData("CStorage");
    }
}
