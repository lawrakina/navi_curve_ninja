using System;
using System.Linq;
using Core.UI.Screens.Base;
using Depra.Toolkit.RootProviders.Runtime.DifferentTypes;
using Misc.RootProviders.Runtime.Base;
using NavySpade.Modules.Sound.Runtime.Core;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.UI.Popups.Abstract
{
    public abstract class Popup : MonoBehaviour
    {
        [Serializable]
        public class StateEvents
        {
            [field: SerializeField] public UnityEvent Opened { get; private set; }
            [field: SerializeField] public UnityEvent Closed { get; private set; }

            [field: Space] [field: SerializeField] public UnityEvent ClosedByDefault { get; private set; }

            public event Action<string> ClosedByCondition;
            [field: SerializeField] public string[] CloseCallbacks { get; private set; }

            public void OnOpen() => Opened.Invoke();
            public void OnClose() => Closed.Invoke();

            internal void InvokeCloseWithCallback(string callback)
            {
                ClosedByCondition?.Invoke(callback);
            }
        }

        [SerializeReference, SubclassSelector] private RootProvider _root;
        [field: SerializeField] public StateEvents Callbacks { get; private set; }

        public void Initialize(BaseScreen attachedScreen)
        {
            ParentScreen = attachedScreen;
        }

        public BaseScreen ParentScreen { get; private set; }

        private bool _isClosed;
        private bool _isTempOpen;

        protected virtual void Awake()
        {
            OnAwake();
        }

        protected virtual void Start()
        {
            if (_root == null)
            {
                Callbacks.OnOpen();

                if (_isTempOpen)
                {
                    _isTempOpen = true;
                }
                
                OnStart();
                Debug.LogError("root provider equal null", this);
                return;
            }

            _root.TryShow(() =>
            {
                Callbacks.OnOpen();

                if (_isTempOpen)
                {
                    _isTempOpen = true;
                }
            });
            OnStart();
        }

        public abstract void OnAwake();
        public abstract void OnStart();
        
        public void CloseMute(bool isInternalClose = false)
        {
            if (ParentScreen)
            {
                ParentScreen.ClosePopup();
            }

            if (_root != null)
            {
                _root.TryHide(CloseActions);
            }
            else
            {
                CloseActions();
            }

            void CloseActions()
            {
                Callbacks.OnClose();

                if (isInternalClose == false)
                {
                    Callbacks.ClosedByDefault.Invoke();
                }
                    
                Destroy(gameObject);
            }
        }

        public void Close()
        {
            Close(false);
        }

        public void Close(bool isInternalClose)
        {
            if (_isClosed)
            {
                return;
            }

            _isClosed = true;
            CloseMute();

            SoundPlayer.PlaySoundFx("Click");
        }

        public void CloseWithTransition(string transition)
        {
            if (Callbacks.CloseCallbacks.Contains(transition) == false)
            {
                Debug.LogError($"{transition} not found in the popup");
            }
            
            Callbacks.InvokeCloseWithCallback(transition);
            Close();
        }

        private void Reset()
        {
            _root = new TransformRoot();
        }
    }
}