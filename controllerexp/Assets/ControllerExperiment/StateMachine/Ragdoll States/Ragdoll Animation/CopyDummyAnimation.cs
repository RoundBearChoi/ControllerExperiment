using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class CopyDummyAnimation : BaseState
    {
        public override void OnEnter()
        {
            subComponentProcessor.SetEntity(SetRagdoll.START_ANIMATING);
        }

        public override void ProcStateFixedUpdate()
        {
            subComponentProcessor.SetEntity(SetRagdoll.COPY_DUMMY_ANIMATION);

            int t = subComponentProcessor.GetInt(RagdollInt.RAGDOLL_ANIMATION_STATE);

            if (t == (int)RagdollAnimationState.LIFELESS_RAGDOLL)
            {
                stateProcessor.TransitionTo(typeof(LifelessRagdoll));
            }
        }
    }
}