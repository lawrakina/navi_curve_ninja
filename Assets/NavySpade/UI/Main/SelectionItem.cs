using UnityEngine;
using UnityEngine.Events;

namespace Core.UI.Main
{
    public class SelectionItem : UIBehaviour
    {
        public UnityEvent Selected;
        public UnityEvent Deselected;

        private SelectionContext _context;

        private void Awake()
        {
            FindContext();
        }

        public void FindContext()
        {
            var parent = transform;
            while (parent != null)
            {
                _context = parent.GetComponent<SelectionContext>();
                
                if(_context != null)
                    break;

                parent = parent.parent;
            }
            
            if(_context == null)
                UnityEngine.Debug.LogWarning("у выбераемого айтема нету контекста", this);
        }

        public void Select()
        {
            if(_context == null)
                return;
            
            _context.Select(this);
        }
    }
}