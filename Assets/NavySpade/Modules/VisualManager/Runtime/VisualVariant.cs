using System.Collections.Generic;
using NavySpade.Modules.Visual.Runtime.Data;
using UnityEngine;

namespace NavySpade.Modules.Visual.Runtime
{
    public class VisualVariant : MonoBehaviour
    {
        [SerializeField] private VisualData _visual;
        
        public bool IsUsed => VisualManager.SelectedVisual == _visual;

        private static HashSet<VisualVariant> _visuals;

        private void Awake()
        {
            //init
            if (_visuals == null)
            {
                _visuals = new HashSet<VisualVariant>();
                VisualManager.OnChangeVisual += UpdateAllVisuals;
            }
        }

        private void Start()
        {
            ChangeVisual();
            _visuals.Add(this);
        }

        private void OnDestroy()
        {
            _visuals.Remove(this);
        }

        private static void UpdateAllVisuals(VisualData visual)
        {
            foreach (var variant in _visuals)
            {
                variant.ChangeVisual();
            }
        }
        
        private void ChangeVisual()
        {
            gameObject.SetActive(IsUsed);
        }
    }
}