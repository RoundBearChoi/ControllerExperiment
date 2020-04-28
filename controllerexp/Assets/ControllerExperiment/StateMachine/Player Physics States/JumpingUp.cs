using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States.Player
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