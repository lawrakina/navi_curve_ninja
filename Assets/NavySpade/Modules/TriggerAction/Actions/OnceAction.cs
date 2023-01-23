using UnityEngine;

namespace Utils.Triggers.Actions
{
    // Utility action that will call another action,
    // but just once, then silently ignore all future action calls.
    public class OnceAction : ActionBase
    {
        [SerializeField] private ActionBase _action;
        [field: SerializeField] public bool IsUsed { get; private set; }

        public override void Fire()
        {
            if (IsUsed)
                return;

            IsUsed = true;
            _action.Fire();
        }
    }
}
