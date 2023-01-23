using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Responses
{
    public class DebugSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        public void OnSelect(Transform selection)
        {
            Debug.Log($"{selection.name} selected");
        }

        public void OnDeselect(Transform selection)
        {
            Debug.Log($"{selection.name} deselected");
        }
    }
}