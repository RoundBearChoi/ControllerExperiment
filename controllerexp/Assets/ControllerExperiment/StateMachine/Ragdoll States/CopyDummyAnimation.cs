using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.States
{
    public class CopyDummyAnimation : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            owner.subComponentProcessor.SetDic[SetRagdoll.COPY_DUMMY_ANIMATION]();
        }
    }
}