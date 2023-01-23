using System;
using UnityEngine;

namespace NavySpade.UI.SafeArea
{
    public class SafeAreaController : MonoBehaviour
    {
        public static SafeAreaController Instance = default;

        public SafeArea Area = new SafeArea();
    
        [NonSerialized] 
        public SafeArea EditorSafeZone = new SafeArea();

        private SafeArea _prevNotch = new SafeArea();
    
        private void Start()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    
        private SafeArea CalculateNotchZone()
        {
            var rect = Screen.safeArea;
            var res = new SafeArea();
            res.Left = rect.x;
            res.Top = Screen.height - rect.y - rect.height;
            res.Right = Screen.width - rect.x - rect.width;
            res.Bottom = rect.y;
            return res;
        }

        private void Update()
        {
            Area = CalculateNotchZone();

            if (SafeArea.IsEqual(Area, _prevNotch)) 
                return;
        
            _prevNotch = Area;
            Reapply();
        }

        private void Reapply()
        {
            var arr = FindObjectsOfType<global::NavySpade.UI.SafeArea.SafeArea>();
            foreach (var el in arr)
                el.Apply();
        }
    
        public struct SafeArea
        {
            public float Left;
            public float Top;
            public float Right;
            public float Bottom;

            public SafeArea(float left, float top, float right, float bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public static bool IsEqual(SafeArea a1, SafeArea a2) => a1.Left == a2.Left && a1.Top == a2.Top &&
                                                                    a1.Right == a2.Right && a1.Bottom == a2.Bottom;
        
            public override string ToString() => $"{Left} {Top} {Right} {Bottom}";
        }
    }
}
