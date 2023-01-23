using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Inherit from ActionBase and override the Fire() method in your class with your payload code.
    /// </summary>
    public class ActionBase : MonoBehaviour
    {
        public virtual void Fire()
        {
            Debug.Log("Override ActionBase!");
        }
    }
}
