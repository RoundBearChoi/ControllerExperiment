using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.States.Player
{
    public class WaitingToJump : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            if (owner.rbody.velocity.y > 0f)
            {
                owner.subComponentProcessor.SetDic[SetPlayer.CANCEL_HORIZONTAL_ANGULAR_VELOCITY]();
                owner.stateProcessor.TransitionTo(typeof(JumpingUp));
            }
        }
    }
}