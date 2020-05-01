using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.States.Player
{
    public class WaitingToJump : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            if (stateProcessor.owner.rbody.velocity.y > 0f)
            {
                subComponentProcessor.SetEntity(SetPlayer.CANCEL_HORIZONTAL_ANGULAR_VELOCITY);
                stateProcessor.TransitionTo(typeof(JumpingUp));
            }
            else
            {
                // temp
                stateProcessor.TransitionTo(typeof(GroundIdle));
            }
        }
    }
}