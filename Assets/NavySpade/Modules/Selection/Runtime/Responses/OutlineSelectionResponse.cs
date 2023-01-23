using NavySpade.Modules.Selection.Runtime.SceneInteraction;
using UnityEngine;

namespace NavySpade.Modules.Selection.Runtime.Responses
{
    public class OutlineSelectionResponse : MonoBehaviour, ISelectionResponse
    {
        public void OnSelect(Transform selection)
        {
            if (selection.TryGetComponent(out OutlineSaver saver))
            {
                saver.Outline.enabled = true;
            }
        }

        public void OnDeselect(Transform selection)
        {
            if (selection.TryGetComponent(out OutlineSaver saver))
            {
                saver.Outline.enabled = false;
            }
        }
    }
}
