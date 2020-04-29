using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Player;

namespace ControllerExperiment.States.Player
{
    public class Landing : BaseState
    {
        public override void OnEnter()
        {
            owner.subComponentProcessor.SetEntity(SetPlayer.CANCEL_HORIZONTAL_VELOCITY);
            owner.subComponentProcessor.SetEntity(SetPlayer.CANCEL_HORIZONTAL_ANGULAR_VELOCITY);
        }

        public override void ProcStateFixedUpdate()
        {
            owner.stateProcessor.TransitionTo(typeof(OnGround));
        }
    }
}