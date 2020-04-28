using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.States
{
    public class OnGround : BaseState
    {
        [Header("Debug")]
        public float GroundSpeed = 3.5f;

        public override void OnEnter()
        {
            owner.subComponentProcessor.SetFloatDic[SetPlayerFloat.TARGET_WALKSPEED](GroundSpeed);
        }

        public override void ProcStateFixedUpdate()
        {
            owner.subComponentProcessor.ProcDic[SetPlayer.WALK_TO_TARGET_DIRECTION]();
            owner.subComponentProcessor.ProcDic[SetPlayer.ROTATE_TO_TARGET_ANGLE]();
            owner.subComponentProcessor.ProcDic[SetPlayer.CANCEL_VERTICAL_VELOCITY]();

            CheckJump();
        }

        void CheckJump()
        {
            owner.subComponentProcessor.ProcDic[SetPlayer.SET_WALK_DIRECTION]();

            bool JumpIsPressed = owner.subComponentProcessor.GetBoolDic[GetPlayerBool.PRESSED_JUMP]();

            if (JumpIsPressed)
            {
                owner.subComponentProcessor.ProcDic[SetPlayer.ADD_JUMP_FORCE]();
                owner.stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }
    }
}