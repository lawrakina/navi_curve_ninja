using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that when called will randomly choose one of the attached actions, and fire it.
    /// This is useful for introducing a little randomness.
    /// </summary>
    public class RandomSampleAction : ActionBase
    {
        [SerializeField] private ActionBase[] _actions;

        public override void Fire()
        {
            RandomChoiceAction.Fire();
        }

        private ActionBase RandomChoiceAction => _actions[Random.Range(0, _actions.Length)];
    }
}