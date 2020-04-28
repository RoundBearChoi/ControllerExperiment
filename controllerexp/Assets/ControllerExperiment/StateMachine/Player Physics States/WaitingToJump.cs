using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.PhysicsState
{
    public class WaitingToJump : PhysicsState
    {
        public override void ProcStateFixedUpdate()
        {
            if (control.rbody.velocity.y > 0f)
            {
                control.subComponentProcessor.ProcDic[PlayerProcess.CANCEL_HORIZONTAL_ANGULAR_VELOCITY]();
                control.stateProcessor.TransitionTo(typeof(JumpingUp));
            }
        }
    }
}