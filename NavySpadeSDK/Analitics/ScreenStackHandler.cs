using System.Collections;
using System.Collections.Generic;
using AnalyticsEnums_;
using UnityEngine;

public class ScreenStackHandler
{
    private static ScreenStackHandler Instance;

    public static ScreenStackHandler GetInstance
    {
        get
        {
            if(Instance == null) Instance = new ScreenStackHandler();
            return Instance;
        }
    }
    
    public Stack<ScreenType> ScreensStack = new Stack<ScreenType>(); 
    public ScreenStackHandler()
    {
        
    }
    public void AddElement(ScreenType type)
    {
//        Debug.Log(type);
        ScreensStack.Push(type);
    }

    public string FormateData()
    {
        var localStack = new Stack<ScreenType>();
        var value = ""; 
        
        for (int i = 0; i < 5; i++)
        {
            if (ScreensStack.Count > 0)
            {
                var element = ScreensStack.Pop();
                value += (i == 0 ? "" : "_") + AnalyticsEnums.GetScreenCode(element);
                localStack.Push(element);
            }
            else
            {
                value += (i == 0 ? "" : "_") + AnalyticsEnums.GetScreenCode(ScreenType.Default_Empty);
            }
        }

        while (localStack.Count>0)
        {
            ScreensStack.Push(localStack.Pop());
        }
         
        return value;
    }
    
    
}
