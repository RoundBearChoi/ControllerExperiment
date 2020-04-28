using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public class JumpingUp : PhysicsState
    {
        public override void ProcStateFixedUpdate()
        {
            if (control.rbody.velocity.y < 0f)
            {
                control.stateProcessor.TransitionTo(typeof(FreeFall));
            }
        }
    }
}