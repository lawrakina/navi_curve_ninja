using NavySpade.Modules.Selection.Runtime.Interfaces;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Abstract
{
    public abstract class SelectionContext<T> : MonoBehaviour, ISelectionContext<T> where T : Component
    {
        public abstract void Select(T item);

        public abstract void Deselect(T item);
    }
}