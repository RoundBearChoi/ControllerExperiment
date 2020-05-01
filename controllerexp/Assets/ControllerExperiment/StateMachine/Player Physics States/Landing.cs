using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.States.Player
{
    public class Landing : BaseState
    {
        public override void OnEnter()
        {
            subComponentProcessor.SetEntity(SetPlayer.CANCEL_HORIZONTAL_VELOCITY);
            subComponentProcessor.SetEntity(SetPlayer.CANCEL_HORIZONTAL_ANGULAR_VELOCITY);
        }

        public override void ProcStateFixedUpdate()
        {
            stateProcessor.TransitionTo(typeof(GroundIdle));
        }
    }
}