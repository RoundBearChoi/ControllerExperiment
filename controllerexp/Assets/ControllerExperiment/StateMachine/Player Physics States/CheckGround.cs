using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Player;

namespace ControllerExperiment.States.Player
{
    public class CheckGround : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            bool isGrounded = owner.subComponentProcessor.GetBoolDic[GetPlayerBool.IS_GROUNDED]();

            if (!isGrounded)
            {
                owner.stateProcessor.TransitionTo(typeof(FreeFall));
            }
            else 
            {
                owner.stateProcessor.TransitionTo(typeof(OnGround));
            }
        }
    }
}