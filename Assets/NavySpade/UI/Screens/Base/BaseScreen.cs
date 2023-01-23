using System;
using System.Collections.Generic;
using Core.UI.Popups;
using NavySpade.UI.Popups.Abstract;
using UnityEngine;
using UnityEngine.Assertions;

namespace Core.UI.Screens.Base
{
    public class BaseScreen : MonoBehaviour
    {
        public static event Action<Popup> PopupOpened;

        [SerializeField] private protected RectTransform popupsParentToScale;

        public readonly Stack<Popup> CurrentPopupsP = new Stack<Popup>();

        private Stack<PopupBackground> _currentPanelsP = new Stack<PopupBackground>();

        public static PopupBackground CurrentBackground { get; private set; }

        protected virtual void Awake()
        {
        }

        public T OpenPopup<T>(T prefab, Action<Popup> onOpened = null, Action onCloseByDefault = null, bool skipBlackButton = false, bool needFast = false) where T : Popup
        {
            return InitAndShowPopup<T>(prefab, onOpened, onCloseByDefault, skipBlackButton, needFast);
        }

        /// <summary>
        /// open popup function
        /// load popup from resources and show it
        /// </summary>
        /// <param name="popupName">name popup in resources</param>
        /// <param name="onCloseByDefault">close function</param>
        /// <param name="skipBlackButton">whether to disable clicking on the black screen</param>
        /// <param name="onOpened">opening function</param>
        /// <typeparam name="T">type of popup</typeparam>
        public T OpenPopup<T>(string popupName, Action<T> onOpened = null, Action onCloseByDefault = null, bool skipBlackButton = false, bool needFast = false) where T : Popup
        {
            var prefab = PopupsConfig.Instance.GetPopup<T>(popupName);

            return InitAndShowPopup(prefab, onOpened, onCloseByDefault, skipBlackButton, needFast);
        }

        public T OpenPopup<T>(Action<T> onOpened = null, Action onCloseByDefault = null, bool skipBlackButton = false, bool needFast = false) where T : Popup
        {
            var prefab = PopupsConfig.Instance.GetPopup<T>();
            
            return InitAndShowPopup<T>(prefab, onOpened, onCloseByDefault, skipBlackButton, needFast);
        }

        public void FastClose()
        {
            if (CurrentPopupsP.Count > 0)
            {
                var topmostPopup = CurrentPopupsP.Pop();


                if (topmostPopup == null)
                    return;
            }

            if (_currentPanelsP.Count > 0)
            {
                var topmostPanel = _currentPanelsP.Pop();
                if (topmostPanel != null && topmostPanel)
                {
                    if (topmostPanel && topmostPanel != null)
                        Destroy(topmostPanel.gameObject);
                }
            }
        }

        public void ClosePopup()
        {
            if (CurrentPopupsP.Count > 0)
            {
                var topmostPopup = CurrentPopupsP.Pop();
                if (topmostPopup == null)
                    return;
            }

            if (_currentPanelsP.Count > 0)
            {
                var topmostPanel = _currentPanelsP.Pop();
                if (topmostPanel != null && topmostPanel)
                {
                    topmostPanel.OnClose();
                }
            }
        }
        
        private T InitAndShowPopup<T>(T popupPrefab, Action<T> onOpened = null, Action onCloseByDefault = null, bool buttonCloseNeed = false, bool needFast = false) where T : Popup
        {
            if(CurrentBackground)
                CurrentBackground.OnClose();
            // if (CurrentBackground == null){

            // }

            var bgPrefab = PopupsConfig.Instance.PopupsBackground;
            var bg = Instantiate(bgPrefab, popupsParentToScale.transform, false);

            bg.OnOpen();

            CurrentBackground = bg;
            _currentPanelsP.Push(bg);
            
            var popup = Instantiate(popupPrefab, popupsParentToScale.transform, false);
            Assert.IsNotNull(popup);
            popup.Initialize(this);
            
            if (needFast)
            {
                if (popup.TryGetComponent<Animator>(out var anim)) anim.speed = 100f;
            }
            else
            {
                if (popup.TryGetComponent<Animator>(out var anim)) anim.speed = 1f;
            }

            if(onCloseByDefault != null)
                popup.Callbacks.ClosedByDefault.AddListener(onCloseByDefault.Invoke);

            onOpened?.Invoke(popup);

            if (!buttonCloseNeed)
            {
                CurrentBackground.ClickCallback = popup.Close;
            }

            CurrentPopupsP.Push(popup);

            PopupOpened?.Invoke(popup);

            return popup;
        }
    }
}