using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that runs the specified other actions in turn.
    /// </summary>
    public class SeriesAction : ActionBase
    {
        [SerializeField] private ActionBase[] _actions;

        public override void Fire()
        {
            foreach (var action in _actions)
            {
                action.Fire();
            }
        }
    }
}