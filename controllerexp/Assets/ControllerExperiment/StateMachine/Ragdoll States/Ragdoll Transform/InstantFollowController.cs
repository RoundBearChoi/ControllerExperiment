using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class InstantFollowController : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            subComponentProcessor.SetEntity(SetRagdoll.INSTANT_ROTATE_ENTITY);
            subComponentProcessor.SetEntity(SetRagdoll.INSTANT_MOVE_ENTITY);

            RagdollTransformState t = (RagdollTransformState)subComponentProcessor.GetInt(RagdollInt.RAGDOLL_TRANSFORM_STATE);
        
            if (t == RagdollTransformState.NO_FOLLOW)
            {
                stateProcessor.TransitionTo(typeof(NoFollow));
            }
        }
    }
}