using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Modules.Visual.Runtime
{
    public class VisualSwitcher : MonoBehaviour
    {
        [Button]
        public void Switch()
        {
            VisualManager.NextVisual();
        }
    }
}
