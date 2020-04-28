using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States
{
    public class JumpingUp : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            if (owner.rbody.velocity.y < 0f)
            {
                owner.stateProcessor.TransitionTo(typeof(FreeFall));
            }
        }
    }
}