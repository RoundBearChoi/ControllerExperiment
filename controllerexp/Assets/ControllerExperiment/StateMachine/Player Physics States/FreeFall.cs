using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.States.Player
{
    public class FreeFall : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            bool isGrounded = owner.subComponentProcessor.GetBoolDic[GetPlayerBool.IS_GROUNDED]();

            if (isGrounded)
            {
                owner.stateProcessor.TransitionTo(typeof(Landing));
            }
        }
    }
}