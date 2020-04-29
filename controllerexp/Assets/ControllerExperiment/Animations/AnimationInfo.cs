using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Animations
{
    public static class AnimationInfo
    {
        static Dictionary<ControllerEntity, Animator> Animators = new Dictionary<ControllerEntity, Animator>();
        
        public static Animator GetAnimator(ControllerEntity owner)
        {
            FindAnimator(owner);

            if (Animators.ContainsKey(owner))
            {
                return Animators[owner];
            }
            else
            {
                return null;
            }
        }

        static void FindAnimator(ControllerEntity owner)
        {
            if (!Animators.ContainsKey(owner))
            {
                Animator animator = owner.GetComponentInChildren<Animator>();

                if (animator != null)
                {
                    Animators.Add(owner, animator);
                }
            }
        }
    }
}