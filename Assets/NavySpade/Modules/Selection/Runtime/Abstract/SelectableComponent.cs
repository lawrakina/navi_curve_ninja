using System;
using NavySpade.Modules.Selection.Runtime.Interfaces;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Abstract
{
    public abstract class SelectableComponent : MonoBehaviour, ISelectable
    {
        public event Action Selected;
        public event Action Deselected;
        
        public void Select()
        {
            Selected?.Invoke();
        }

        public void Deselect()
        {
            Deselected?.Invoke();
        }
    }
}