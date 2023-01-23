using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public class KeyListener : MonoBehaviour
    {
        [SerializeField] private KeyCode _key = default;
        [SerializeField] private UnityEvent _onPressed = new UnityEvent();

#if UNITY_EDITOR

        private void Update()
        {
            if (Input.GetKeyDown(_key) == false)
                return;

            _onPressed.Invoke();
        }

#endif
    }
}