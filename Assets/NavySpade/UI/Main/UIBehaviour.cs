using UnityEngine;

namespace Core.UI.Main
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBehaviour : MonoBehaviour
    {
        public CanvasGroup Group => _canvasGroup == null ? _canvasGroup = GetComponent<CanvasGroup>() : _canvasGroup;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetActive(bool isActive)
        {
            Group.alpha = isActive ? 1 : 0;
            Group.blocksRaycasts = isActive;
        }
    }
}