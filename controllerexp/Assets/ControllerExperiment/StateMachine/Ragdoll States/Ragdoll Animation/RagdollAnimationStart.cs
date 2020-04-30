using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class RagdollAnimationStart : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            RagdollAnimationState t = (RagdollAnimationState)subComponentProcessor.GetInt(RagdollInt.RAGDOLL_ANIMATION_STATE);

            if (t == RagdollAnimationState.COPY_DUMMY_ANIMATION)
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