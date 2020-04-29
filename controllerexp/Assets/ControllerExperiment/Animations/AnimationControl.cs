using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Animations
{
    public static class AnimationControl
    {
        public static void Play(ControllerEntity entity, string animationName, float normalizedTime)
        {
            Animator animator = AnimationInfo.GetAnimator(entity);

            if (animator == null)
            {
                return;
            }

            if (!IsPlaying(animator, animationName))
            {
                animator.Play(animationName, 0, normalizedTime);
            }
        }

        static bool IsPlaying(Animator animator, string animationName)
        {
            AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

            if (info.IsName(animationName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}