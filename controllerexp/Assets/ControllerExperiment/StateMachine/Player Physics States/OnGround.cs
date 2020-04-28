using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.PhysicsState
{
    public class OnGround : PhysicsState
    {
        [Header("Debug")]
        public float GroundSpeed = 3.5f;

        public override void OnEnter()
        {
            control.scProcessor.SetFloatDic[SetPlayerFloat.TARGET_WALKSPEED](GroundSpeed);
        }

        public override void ProcStateFixedUpdate()
        {
            control.scProcessor.ProcDic[PlayerProcess.WALK_TO_TARGET_DIRECTION]();
            control.scProcessor.ProcDic[PlayerProcess.ROTATE_TO_TARGET_ANGLE]();
            control.CancelVerticalVelocity();

            CheckJump();
        }

        void CheckJump()
        {
            control.scProcessor.ProcDic[PlayerProcess.SET_WALK_DIRECTION]();

            if (control.JumpButtonPressed)
            {
                control.AddJumpForce();
                control.stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }
    }
}