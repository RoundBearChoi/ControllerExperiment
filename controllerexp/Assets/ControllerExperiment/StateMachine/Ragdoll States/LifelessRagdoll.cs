using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Ragdoll;

namespace ControllerExperiment.States.Ragdoll
{
    public class LifelessRagdoll : BaseState
    {
        public override void OnEnter()
        {
            owner.subComponentProcessor.SetDic[SetRagdoll.STOP_ANIMATING]();
        }

        public override void ProcStateFixedUpdate()
        {
            SelectedRagdoll t = (SelectedRagdoll)owner.subComponentProcessor.GetIntDic[GetRagdollInt.DESIRED_RAGDOLL_STATE]();

            if (t == SelectedRagdoll.COPY_DUMMY_ANIMATION)
            {
                owner.stateProcessor.TransitionTo(typeof(CopyDummyAnimation));
            }
        }
    }
}