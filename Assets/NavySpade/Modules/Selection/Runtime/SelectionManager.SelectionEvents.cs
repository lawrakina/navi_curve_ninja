using System;
using UnityEngine.Events;

namespace NavySpade.Modules.Selection.Runtime
{
    public partial class SelectionManager
    {
        [Serializable]
        public class SelectionEvents
        {
            public UnityEvent _onSelected = new UnityEvent();
            public UnityEvent _onSelectionEmpty = new UnityEvent();

            public void OnSelected()
            {
                _onSelected.Invoke();
            }

            public void OnSelectionEmpty()
            {
                _onSelectionEmpty.Invoke();
            }
        }
    }
}