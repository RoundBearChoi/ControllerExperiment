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

            if (t == RagdollTransformState.FOLLOW_DUMMY_POSITION)
            {
                //stateProcessor.TransitionTo(typeof())
            }
            else if (t == RagdollTransformState.DONT_FOLLOW_DUMMY_POSITION)
            {

            }
        }
    }
}