using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States.Player
{
    public class JumpingUp : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            if (stateProcessor.owner.rbody.velocity.y < 0f)
            {
                stateProcessor.TransitionTo(typeof(FreeFall));
            }
        }
    }
}