using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Crossfade queues up the specified animation.
    /// </summary>
    public class QueueAnimationAction : ActionBase
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private string _animationName;

        public override void Fire()
        {
            _animation.CrossFadeQueued(_animationName);
            ;
        }
    }
}