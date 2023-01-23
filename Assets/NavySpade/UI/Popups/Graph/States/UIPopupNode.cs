using System;
using System.Collections.Generic;
using Core.UI.Popups.Abstract;
using Core.UI.Screens.Base;
using NavySpade.UI.Popups.Abstract;

namespace Core.UI.Popups.Graph
{
    [CreateNodeMenu("UI/Popups/Sample")]
    public class UIPopupNode : UIPopupNode<Popup>
    {
    }

    public class UIPopupNode<T> : State where T : Popup
    {
        public T Prefab;

        [Input] public StateTransition Show;

        [Output] public StateTransition OnClose;
        [Output(dynamicPortList = true)] public List<PopupTransition> ExitButtons;

        private Popup _instance;

        private void OnValidate()
        {
            return;
            
            if (Prefab == null)
                return;

            ExitButtons = new List<PopupTransition>(Prefab.Callbacks.CloseCallbacks.Length);

            for (var i = 0; i < ExitButtons.Count; i++)
            {
                var str = Prefab.Callbacks.CloseCallbacks[i];
                var newData = new PopupTransition();
                newData.Target = str;
                
                ExitButtons[i] = newData;
            }
        }

        public override void Run()
        {
            var popup = Graph.Drawing.OpenPopup(Prefab, null,
                () => { Complete(GetOutputPort(nameof(OnClose))); });

            popup.Callbacks.ClosedByCondition += TransitionCallback;

            _instance = popup;
        }

        protected override void OnEnded()
        {
            if (_instance == null)
                return;

            _instance.Callbacks.ClosedByCondition -= TransitionCallback;
        }

        private void TransitionCallback(string callback)
        {
            var index = ExitButtons.FindIndex(c => c.Target == callback);

            if (index < 0)
                throw new ArgumentException(callback);

            Complete(GetOutputPort($"{nameof(ExitButtons)} {index}"));
        }
    }
}