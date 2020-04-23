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
            control.SetFloatDic[SetFunction.TARGETWALKSPEED](GroundSpeed);
        }

        public override void ProcStateFixedUpdate()
        {
            control.ProcDic[PlayerFunction.WALK_TARGETDIRECTION]();
            control.ProcDic[PlayerFunction.ROTATE_TARGETANGLE]();
            control.CancelVerticalVelocity();
        }

        public override void ProcStateUpdate()
        {
            control.ProcDic[PlayerFunction.SET_TARGETWALKDIRECTION]();

            if (control.JumpButtonPressed)
            {
                control.AddJumpForce();
                control.stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }
    }
}