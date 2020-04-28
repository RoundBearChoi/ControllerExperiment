using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.PhysicsState
{
    public class Landing : PhysicsState
    {
        public override void OnEnter()
        {
            control.subComponentProcessor.ProcDic[PlayerProcess.CANCEL_HORIZONTAL_VELOCITY]();
            control.subComponentProcessor.ProcDic[PlayerProcess.CANCEL_HORIZONTAL_ANGULAR_VELOCITY]();
        }

        public override void ProcStateFixedUpdate()
        {
            control.stateProcessor.TransitionTo(typeof(OnGround));
        }
    }
}