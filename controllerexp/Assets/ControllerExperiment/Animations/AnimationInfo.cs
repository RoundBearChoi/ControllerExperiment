using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Animations
{
    public static class AnimationInfo
    {
        static Dictionary<string, Animator> Animators = new Dictionary<string, Animator>();
        
        public static Animator GetAnimator(string objName)
        {
            FindAnimator(objName);

            if (Animators.ContainsKey(objName))
            {
                return Animators[objName];
            }
            else
            {
                return null;
            }
        }

        static void FindAnimator(string objName)
        {
            if (!Animators.ContainsKey(objName))
            {
                GameObject obj = GameObject.Find(objName);
                Animator animator = obj.GetComponent<Animator>();

                if (animator != null)
                {
                    Animators.Add(objName, animator);
                }
            }
        }
    }
}