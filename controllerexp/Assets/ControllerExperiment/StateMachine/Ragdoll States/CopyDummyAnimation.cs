using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Ragdoll;

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

            SelectedRagdoll t = (SelectedRagdoll)subComponentProcessor.GetInt(RagdollInt.DESIRED_RAGDOLL_STATE);

            if (t == SelectedRagdoll.LIFELESS_RAGDOLL)
            {
                stateProcessor.TransitionTo(typeof(LifelessRagdoll));
            }
        }
    }
}