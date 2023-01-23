using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.Modules.Utils.Helpers
{
    [RequireComponent(typeof(Canvas))]
    public class CanvasHelper : MonoBehaviour
    {
        public static UnityEvent onOrientationChange = new UnityEvent();
        public static UnityEvent onResolutionChange = new UnityEvent();
        public static bool isLandscape { get; private set; }
        public RectTransform upPanel;
 
        private static List<CanvasHelper> helpers = new List<CanvasHelper>();
 
        private static bool screenChangeVarsInitialized = false;
        private static ScreenOrientation lastOrientation = ScreenOrientation.Portrait;
        private static Vector2 lastResolution = Vector2.zero;
        private static Rect lastSafeArea = Rect.zero;
 
        public Canvas canvas;
        public RectTransform rectTransform;
 
        public RectTransform safeAreaTransform;
        public RectTransform safeAreaTransformNotDown;
   
        void Awake()
        {
            if(!helpers.Contains(this))
                helpers.Add(this);
     
             
            if(!screenChangeVarsInitialized)
            {
                lastOrientation = Screen.orientation;
                lastResolution.x = Screen.width;
                lastResolution.y = Screen.height;
                lastSafeArea = Screen.safeArea;
     
                screenChangeVarsInitialized = true;
            
            }
            ApplySafeArea();
        }
 
        void Start()
        {
            //ApplySafeArea();
        }
 
        void Update()
        {
            if(helpers[0] != this)
                return;
         
            if(Application.isMobilePlatform)
            {
                if(Screen.orientation != lastOrientation)
                    OrientationChanged();
         
                if(Screen.safeArea != lastSafeArea)
                    SafeAreaChanged();
            }
            else
            {
                //resolution of mobile devices should stay the same always, right?
                // so this check should only happen everywhere else
                if(Screen.width != lastResolution.x || Screen.height != lastResolution.y)
                    ResolutionChanged();
            }
        }

    
        void ApplySafeArea()
        {
            if(safeAreaTransform == null)
                return;
     
            var safeArea = Screen.safeArea;
     
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
#if UNITY_SAFEZONE

        anchorMin = Vector3.zero;
        anchorMax.x = Screen.width;
        anchorMax.y = Screen.height * 0.95f;
#endif

        
            anchorMin.x /= canvas.pixelRect.width;
            anchorMin.y /= canvas.pixelRect.height;
            anchorMax.x /= canvas.pixelRect.width;
            anchorMax.y /= canvas.pixelRect.height;
            if (upPanel != null)
            {
                var newAnchorMax = anchorMax;
                newAnchorMax.x = 0;
                newAnchorMax.y = anchorMax.y;
                upPanel.anchorMin = newAnchorMax;
                upPanel.anchorMax = new Vector2(1, 1);
            }

            if (safeAreaTransformNotDown != null)
            {
                var jonAnchor = safeAreaTransformNotDown.anchorMin;
                var newMinAnchor = anchorMin;
                newMinAnchor.x = jonAnchor.x;
                safeAreaTransformNotDown.anchorMin = newMinAnchor;
                safeAreaTransformNotDown.anchorMax = anchorMax;
            }
     
            safeAreaTransform.anchorMin = anchorMin;
            safeAreaTransform.anchorMax = anchorMax;
        

        }
 
        void OnDestroy()
        {
            if(helpers != null && helpers.Contains(this))
                helpers.Remove(this);
        }
 
        private static void OrientationChanged()
        {
            //Debug.Log("Orientation changed from " + lastOrientation + " to " + Screen.orientation + " at " + Time.time);
     
            lastOrientation = Screen.orientation;
            lastResolution.x = Screen.width;
            lastResolution.y = Screen.height;
     
            isLandscape = lastOrientation == ScreenOrientation.LandscapeLeft || lastOrientation == ScreenOrientation.LandscapeRight || lastOrientation == ScreenOrientation.LandscapeLeft;
            onOrientationChange.Invoke();
     
        }
 
        private static void ResolutionChanged()
        {
            if(lastResolution.x == Screen.width && lastResolution.y == Screen.height)
                return;
     
            //Debug.Log("Resolution changed from " + lastResolution + " to (" + Screen.width + ", " + Screen.height + ") at " + Time.time);
     
            lastResolution.x = Screen.width;
            lastResolution.y = Screen.height;
     
            isLandscape = Screen.width > Screen.height;
            onResolutionChange.Invoke();
        }
 
        private static void SafeAreaChanged()
        {
            if(lastSafeArea == Screen.safeArea)
                return;
     
            //Debug.Log("Safe Area changed from " + lastSafeArea + " to " + Screen.safeArea.size + " at " + Time.time);
     
            lastSafeArea = Screen.safeArea;
     
            for (int i = 0; i < helpers.Count; i++)
            {
                helpers[i].ApplySafeArea();
            }
        }
 
        public static Vector2 GetCanvasSize()
        {
            return helpers[0].rectTransform.sizeDelta;
        }
 
        public static Vector2 GetSafeAreaSize()
        {
            for (int i = 0; i < helpers.Count; i++)
            {
                if(helpers[i].safeAreaTransform != null)
                {
                    return helpers[i].safeAreaTransform.sizeDelta;
                }
            }
     
            return GetCanvasSize();
        }
    }
}