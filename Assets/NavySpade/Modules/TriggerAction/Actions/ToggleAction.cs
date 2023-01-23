using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that alternates between firing one action or another.
    /// Useful for enabling and disabling menus.
    /// </summary>
    public class ToggleAction : ActionBase
    {
        [field: SerializeField] public bool IsToggled { get; private set; }
        [SerializeField] private ActionBase _enabledAction;
        [SerializeField] private ActionBase _disabledAction;

        public override void Fire()
        {
            IsToggled = !IsToggled;

            if (IsToggled)
                _enabledAction.Fire();
            else
                _disabledAction.Fire();
        }
    }
}