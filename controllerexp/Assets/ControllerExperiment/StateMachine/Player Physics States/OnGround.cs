using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Player;

namespace ControllerExperiment.States.Player
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
            owner.subComponentProcessor.SetDic[SetPlayer.WALK_TO_TARGET_DIRECTION]();
            owner.subComponentProcessor.SetDic[SetPlayer.ROTATE_TO_TARGET_ANGLE]();
            owner.subComponentProcessor.SetDic[SetPlayer.CANCEL_VERTICAL_VELOCITY]();

            CheckJump();
        }

        void CheckJump()
        {
            owner.subComponentProcessor.SetDic[SetPlayer.SET_WALK_DIRECTION]();

            bool JumpIsPressed = owner.subComponentProcessor.GetBool(PlayerBool.PRESSED_JUMP);

            if (JumpIsPressed)
            {
                owner.subComponentProcessor.SetDic[SetPlayer.ADD_JUMP_FORCE]();
                owner.stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }
    }
}