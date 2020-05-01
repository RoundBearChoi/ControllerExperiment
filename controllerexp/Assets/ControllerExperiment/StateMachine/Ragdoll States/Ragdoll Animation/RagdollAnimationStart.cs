using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class RagdollAnimationStart : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            int t = subComponentProcessor.GetInt(RagdollInt.RAGDOLL_ANIMATION_STATE);

            if (t == (int)RagdollAnimationState.COPY_DUMMY_ANIMATION)
            {
                stateProcessor.TransitionTo(typeof(CopyDummyAnimation));
            }
            else
            {
                stateProcessor.TransitionTo(typeof(LifelessRagdoll));
            }
        }
    }
}