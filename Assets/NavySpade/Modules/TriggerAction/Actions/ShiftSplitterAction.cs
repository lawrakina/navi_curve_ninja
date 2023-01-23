using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// When the action is fired and the user is holding down shift, fires one action, otherwise fires another action.
    /// Perhaps a little overly specialized for this collection.
    /// Can be converted to a ButtonAction.
    /// </summary>
    public class ShiftSplitterAction : ActionBase
    {
        [SerializeField] private ActionBase _shiftAction;
        [SerializeField] private ActionBase _standardAction;

        public override void Fire()
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                _shiftAction.Fire();
            else
                _standardAction.Fire();
        }
    }
}
