using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public class Landing : PhysicsState
    {
        public override void OnEnter()
        {
            control.ComponentProcessor.ProcDic[PlayerFunction.CANCEL_HORIZONTAL_VELOCITY]();
            control.ComponentProcessor.ProcDic[PlayerFunction.CANCEL_HORIZONTAL_ANGULAR_VELOCITY]();
        }

        public override void ProcStateFixedUpdate()
        {
            control.stateProcessor.TransitionTo(typeof(OnGround));
        }
    }
}