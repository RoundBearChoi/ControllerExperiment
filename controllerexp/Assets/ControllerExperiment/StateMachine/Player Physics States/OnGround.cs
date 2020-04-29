using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Player;
using ControllerExperiment.Animations;

namespace ControllerExperiment.States.Player
{
    public class OnGround : BaseState
    {
        [Header("Debug")]
        public float GroundSpeed = 3.5f;

        public override void OnEnter()
        {
            owner.subComponentProcessor.SetFloat(PlayerFloat.SET_TARGET_WALK_SPEED, GroundSpeed);
        }

        public override void ProcStateFixedUpdate()
        {
            owner.subComponentProcessor.SetEntity(SetPlayer.SET_WALK_DIRECTION);
            owner.subComponentProcessor.SetEntity(SetPlayer.ROTATE_TO_TARGET_ANGLE);
            owner.subComponentProcessor.SetEntity(SetPlayer.WALK_TO_TARGET_DIRECTION);
            owner.subComponentProcessor.SetEntity(SetPlayer.CANCEL_VERTICAL_VELOCITY);

            CheckWalk();
            CheckJump();
        }

        void CheckJump()
        {
            bool JumpIsPressed = owner.subComponentProcessor.GetBool(PlayerBool.PRESSED_JUMP);

            if (JumpIsPressed)
            {
                owner.subComponentProcessor.SetEntity(SetPlayer.ADD_JUMP_FORCE);
                owner.stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }

        void CheckWalk()
        {
            float s = owner.subComponentProcessor.GetFloat(PlayerFloat.GET_TARGET_WALK_SPEED);

            if (s > 0.0001f)
            {
                AnimationControl.Play(owner, AnimationNames.Walking, 0f);
            }
            else
            {
                AnimationControl.Play(owner, AnimationNames.Idle, 0f);
            }
        }
    }
}