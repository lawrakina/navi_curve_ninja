using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI.Popups
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopupBackground : MonoBehaviour, IPointerDownHandler
    {
        [Serializable]
        public class CallbackEvents
        {
            [field: SerializeField] public UnityEvent OnOpen { get; private set; }
            [field: SerializeField] public UnityEvent OnClose { get; private set; }
        }

        [field: SerializeField] public CallbackEvents Callbacks { get; private set; }

        private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup => _canvasGroup ??= GetComponent<CanvasGroup>();

        public Action ClickCallback { get; set; }
        
        public void OnOpen()
        {
            Callbacks.OnOpen.Invoke();
        }

        public void OnClose()
        {
            Callbacks.OnClose.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ClickCallback?.Invoke();
        }
    }
}