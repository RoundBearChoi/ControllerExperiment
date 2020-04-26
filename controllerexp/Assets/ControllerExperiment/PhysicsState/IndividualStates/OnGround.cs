using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public class OnGround : PhysicsState
    {
        [Header("Debug")]
        public float GroundSpeed = 3.5f;

        public override void OnEnter()
        {
            control.PlayerComponents.SetFloatDic[SetFunction.TARGETWALKSPEED](GroundSpeed);
        }

        public override void ProcStateFixedUpdate()
        {
            control.PlayerComponents.ProcDic[PlayerFunction.WALK_TARGETDIRECTION]();
            control.PlayerComponents.ProcDic[PlayerFunction.ROTATE_TARGETANGLE]();
            control.CancelVerticalVelocity();

            CheckJump();
        }

        void CheckJump()
        {
            control.PlayerComponents.ProcDic[PlayerFunction.SET_TARGETWALKDIRECTION]();

            if (control.JumpButtonPressed)
            {
                control.AddJumpForce();
                control.stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }
    }
}