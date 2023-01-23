using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Utility action that will fire the attached action a given percentage of the time.
    /// This is useful for introducing a little randomness into your game.
    /// </summary>
    public class RandomChanceAction : ActionBase
    {
        [field: SerializeField] public int PercentageChance { get; private set; }
        [SerializeField] private ActionBase _action;

        public override void Fire()
        {
            if (Random.Range(0, 100) < PercentageChance - 1)
                _action.Fire();
        }
    }
}
