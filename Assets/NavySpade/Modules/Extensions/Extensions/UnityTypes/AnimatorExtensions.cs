using UnityEngine;

namespace Toolkit.Extensions.UnityTypes
{
    public static class AnimatorExtensions
    {
        public static void PlayAnimator(this Animator animator, string name, int layer, float time)
        {
            if (animator && animator.isActiveAndEnabled)
            {
                animator.Play(name, layer, time);
            }
        }

        public static void PlayOrResumeAnimator(this Animator animator, string name)
        {
            if (animator && animator.isActiveAndEnabled)
            {
                animator.Play(name);
            }
        }
    }
}