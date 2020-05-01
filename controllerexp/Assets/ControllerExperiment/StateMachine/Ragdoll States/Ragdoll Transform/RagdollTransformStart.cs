using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class RagdollTransformStart : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            int t = subComponentProcessor.GetInt(RagdollInt.RAGDOLL_TRANSFORM_STATE);

            if (t == (int)RagdollTransformState.INSTANT_FOLLOW)
            {
                stateProcessor.TransitionTo(typeof(InstantFollowPlayerController));
            }
            else if (t == (int)RagdollTransformState.NO_FOLLOW)
            {
                stateProcessor.TransitionTo(typeof(NoFollow));
            }
        }
    }
}