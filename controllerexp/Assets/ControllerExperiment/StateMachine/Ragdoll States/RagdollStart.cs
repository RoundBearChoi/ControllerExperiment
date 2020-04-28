using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.States
{
    public class RagdollStart : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            if (owner.subComponentProcessor.GetBoolDic[GetRagdollBool.DUMMY_IS_SET]())
            {
                owner.stateProcessor.TransitionTo(typeof(CopyDummyAnimation));
            }
            else
            {

            }
        }
    }
}