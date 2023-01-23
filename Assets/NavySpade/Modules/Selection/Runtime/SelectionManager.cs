using NavySpade.Modules.Selection.Runtime.RayProviders;
using NavySpade.Modules.Selection.Runtime.Responses;
using NavySpade.Modules.Selection.Runtime.Selectors;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime
{
    public partial class SelectionManager : MonoBehaviour
    {
        [SerializeField] private SelectionEvents _events;
        
        private IRayProvider _rayProvider;
        private ISelector _selector;
        private ISelectionResponse _selectionResponse;

        private Transform _currentSelection;

        private void Awake()
        {
            _rayProvider = GetComponent<IRayProvider>();
            _selector = GetComponent<ISelector>();
            _selectionResponse = GetComponent<ISelectionResponse>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) == false)
            {
                return;
            }

            _selector.Check(_rayProvider.CreateRay());
            var newSelection = _selector.GetSelection();

            if (_currentSelection && newSelection == _currentSelection)
            {
                return;
            }

            if (_currentSelection != null)
            {
                _selectionResponse.OnDeselect(_currentSelection);
            }

            _currentSelection = newSelection;

            if (_currentSelection == null)
            {
                _events.OnSelectionEmpty();
            }
            else
            {
                _selectionResponse.OnSelect(_currentSelection);
                _events.OnSelected();
            }
        }
    }
}