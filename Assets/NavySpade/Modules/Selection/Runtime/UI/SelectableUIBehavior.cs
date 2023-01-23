using Core.UI.Main;
using NavySpade.Modules.Selection.Runtime.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade.Modules.Selection.Runtime.UI
{
    public class SelectableUIBehavior : UIBehaviour, ISelectable
    {
        [field: SerializeField] public UnityEvent Selected { get; set; }
        [field: SerializeField] public UnityEvent Deselected { get; set; }

        private UIBehaviorSelectionContext _context;

        private void Awake()
        {
            FindContext();
        }

        public void Select()
        {
            if (_context == null)
            {
                return;
            }

            _context.Select(this);
        }

        public void Deselect()
        {
            if (_context == null)
            {
                return;
            }
            
            _context.Deselect(this);
        }
        
        private void FindContext()
        {
            var parent = transform;
            while (parent != null)
            {
                _context = parent.GetComponent<UIBehaviorSelectionContext>();
                if (_context != null)
                {
                    break;
                }

                parent = parent.parent;
            }

            if (_context == null)
            {
                Debug.LogWarning($"[{nameof(SelectableUIBehavior)}] context not found!", this);
            }
        }
    }
}