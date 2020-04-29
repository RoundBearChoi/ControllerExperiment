using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Player;

namespace ControllerExperiment.States.Player
{
    public class FreeFall : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            bool isGrounded = owner.subComponentProcessor.GetBool(PlayerBool.IS_GROUNDED);

            if (isGrounded)
            {
                owner.stateProcessor.TransitionTo(typeof(Landing));
            }
        }
    }
}