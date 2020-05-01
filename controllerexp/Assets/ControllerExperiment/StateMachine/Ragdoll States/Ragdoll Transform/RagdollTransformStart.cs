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
            RagdollTransformState t = (RagdollTransformState)subComponentProcessor.GetInt(RagdollInt.RAGDOLL_TRANSFORM_STATE);

            if (t == RagdollTransformState.INSTANT_FOLLOW)
            {
                stateProcessor.TransitionTo(typeof(InstantFollowController));
            }
            else if (t == RagdollTransformState.NO_FOLLOW)
            {
                stateProcessor.TransitionTo(typeof(NoFollow));
            }
        }
    }
}