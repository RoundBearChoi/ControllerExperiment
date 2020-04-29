using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class RagdollStart : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            SelectedRagdoll t = (SelectedRagdoll)owner.subComponentProcessor.GetInt(RagdollInt.DESIRED_RAGDOLL_STATE);

            if (t == SelectedRagdoll.COPY_DUMMY_ANIMATION)
            {
                owner.stateProcessor.TransitionTo(typeof(CopyDummyAnimation));
            }
            else
            {
                owner.stateProcessor.TransitionTo(typeof(LifelessRagdoll));
            }
        }
    }
}