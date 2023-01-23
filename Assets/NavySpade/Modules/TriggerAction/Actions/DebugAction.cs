using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Simply prints to the log.
    /// Useful for figuring out if a complex chain is working correctly.
    /// </summary>
    public class DebugAction : ActionBase
    {
        public override void Fire()
        {
            Debug.Log("Action is firing on " + gameObject);
        }
    }
}
