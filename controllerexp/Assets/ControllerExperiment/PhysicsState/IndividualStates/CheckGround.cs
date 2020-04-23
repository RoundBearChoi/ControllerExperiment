using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public class CheckGround : PhysicsState
    {
        public override void ProcStateFixedUpdate()
        {
            if (!control.IsGrounded)
            {
                control.stateProcessor.TransitionTo(typeof(Airborne));
            }
            else 
            {
                control.stateProcessor.TransitionTo(typeof(OnGround));
            }
        }
    }
}