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
            owner.subComponentProcessor.SetDic[SetRagdoll.START_ANIMATING]();
        }

        public override void ProcStateFixedUpdate()
        {
            owner.subComponentProcessor.SetDic[SetRagdoll.COPY_DUMMY_ANIMATION]();

            SelectedRagdoll t = (SelectedRagdoll)owner.subComponentProcessor.GetIntDic[GetRagdollInt.DESIRED_RAGDOLL_STATE]();

            if (t == SelectedRagdoll.LIFELESS_RAGDOLL)
            {
                owner.stateProcessor.TransitionTo(typeof(LifelessRagdoll));
            }
        }
    }
}