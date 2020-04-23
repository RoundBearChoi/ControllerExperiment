using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public class FreeFall : PhysicsState
    {
        public override void ProcStateFixedUpdate()
        {
            if (control.IsGrounded)
            {
                control.stateProcessor.TransitionTo(typeof(Landing));
            }
        }
    }
}