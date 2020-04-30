using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.States.Player
{
    public class FreeFall : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            bool isGrounded = subComponentProcessor.GetBool(PlayerBool.IS_GROUNDED);

            if (isGrounded)
            {
                stateProcessor.TransitionTo(typeof(Landing));
            }
        }
    }
}