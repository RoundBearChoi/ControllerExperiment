using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.States
{
    public class Landing : BaseState
    {
        public override void OnEnter()
        {
            owner.subComponentProcessor.SetDic[SetPlayer.CANCEL_HORIZONTAL_VELOCITY]();
            owner.subComponentProcessor.SetDic[SetPlayer.CANCEL_HORIZONTAL_ANGULAR_VELOCITY]();
        }

        public override void ProcStateFixedUpdate()
        {
            owner.stateProcessor.TransitionTo(typeof(OnGround));
        }
    }
}