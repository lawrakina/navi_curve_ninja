using UnityEngine;
using UnityEngine.UI;

namespace NavySpade.Modules.Selection.Runtime.SceneInteraction
{
    public class OutlineSaver : MonoBehaviour
    {
        [field: SerializeField] public Outline Outline { get; private set; }

        private void Start()
        {
            Outline.enabled = false;
        }

        public void Reset()
        {
            Outline = GetComponentInChildren<Outline>();
        }
    }
}
