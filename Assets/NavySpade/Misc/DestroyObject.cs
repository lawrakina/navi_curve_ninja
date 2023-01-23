using NaughtyAttributes;
using UnityEngine;

namespace NavySpade.Misc
{
    public class DestroyObject : MonoBehaviour
    {
        public bool IsDestroySelfObject = true;

        [HideIf(nameof(IsDestroySelfObject))] public GameObject Target;

        public bool HasDelay;
        [ShowIf(nameof(HasDelay))] public float Delay;

        public bool IsAutoDestroy;

        public GameObject DestroyTarget => IsDestroySelfObject ? gameObject : Target;

        private void Start()
        {
            if (IsAutoDestroy)
                Invoke();
        }

        public void Invoke()
        {
            if (HasDelay)
            {
                Destroy(DestroyTarget, Delay);
            }
            else
            {
                Destroy(DestroyTarget);
            }
        }
    }
}