using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelPointerEvent_Analytic : MonoBehaviour
{
    public float sizeX = 0.002f;
    public float sizeY = 0.002f;
    
    void Start()
    {

    }

    private bool isDownFinger;

    void Update()
    {
        
        if (isDownFinger)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDownFinger = false;
                return;
            }

            UpdateAnalyticEvent();
        }

        /*if (!isDownFinger)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDownFinger = true;
                StartAnalyticEvent();
            }
            else 
            if (Input.GetMouseButton(0) && !isDownFinger)
            {
                isDownFinger = true;
                                StartAnalyticEvent();
            }
        }*/
    }

    public void StartAnalyticsEvent()
    {
        isDownFinger = true;
        StartAnalyticEvent();
    }
    
    
    public int Count = 0 ;
    private Vector2 position;
    private Vector2 oldPosition;
    private void StartAnalyticEvent()
    {
        var position = calculate();
        oldPosition = position;
        
        //AnaliticsController.SendFingerOneLevel(GameScreen.Instance.GetLevelInfo, position.x, position.y, GameScreen.Instance.Shooter.ShootCount);
        Count++;
    }

    private void UpdateAnalyticEvent()
    {
        var position = calculate();
        if (Mathf.Abs(position.x - oldPosition.x) > sizeX || Mathf.Abs(position.y - oldPosition.y) > sizeY)
        {
            //AnaliticsController.SendFingerOneLevel(GameScreen.Instance.GetLevelInfo, position.x, position.y,GameScreen.Instance.Shooter.ShootCount);
            Count++;
            oldPosition = position;
        }

        
    }
    public Vector2 calculate()
    {
        var position_ = Input.mousePosition;
        //Debug.LogError("POS " + position_);
        var razreshenie = 0.5625f;
        float screenSpace = (float)Screen.width / (float)Screen.height;
       // Debug.LogError("ScreeSpace " + screenSpace);

        if (screenSpace < razreshenie)
        {
            float height = (float)Screen.width / razreshenie;
            float width = (float)Screen.width;
           // Debug.LogError("height " + height + " width " + width);
   
            float x = position_.x / width;
            float y = position_.y / height;
           // Debug.LogError("X " + x + " Y " + y);

            return new Vector2(x, y);
        }
        else if (screenSpace > razreshenie)
        {
            float newWidth = (float)Screen.height * razreshenie;
            float fullPart = (float)Screen.width - newWidth;
            float halfPart = fullPart/2f;
          //  Debug.LogError("new W " + newWidth + " fullPart " + fullPart + " halfPart " + halfPart);

            float currX = position_.x - halfPart;
            float width = newWidth;
            float height = (float)Screen.height; 
         //   Debug.LogError("currX " + currX + " height " + height + " width " + width);

            float x = currX / width;
            float y = position_.y / height;
          //  Debug.LogError("X " + x + " Y " + y);

            
            
            
            return new Vector2(x, y);
        }
        else
        {
            
            float x = position_.x / (float)Screen.width;
            float y = position_.y / (float)Screen.height;
            
         //   Debug.LogError("X " + x + " Y " + y);
            return new Vector2(x, y);
        }
        
    }
}
