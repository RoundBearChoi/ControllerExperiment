using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Player;
using ControllerExperiment.Animations;

namespace ControllerExperiment.States.Player
{
    public class OnGround : BaseState
    {
        [Header("OnGround Debug")]
        public float GroundSpeed = 3.5f;

        public override void OnEnter()
        {
            subComponentProcessor.SetFloat(PlayerFloat.SET_TARGET_WALK_SPEED, GroundSpeed);
        }

        public override void ProcStateFixedUpdate()
        {
            subComponentProcessor.SetEntity(SetPlayer.SET_WALK_DIRECTION);
            subComponentProcessor.SetEntity(SetPlayer.ROTATE_TO_TARGET_ANGLE);
            subComponentProcessor.SetEntity(SetPlayer.WALK_TO_TARGET_DIRECTION);
            subComponentProcessor.SetEntity(SetPlayer.CANCEL_VERTICAL_VELOCITY);

            CheckWalk();
            CheckJump();
        }

        void CheckJump()
        {
            bool JumpIsPressed = subComponentProcessor.GetBool(PlayerBool.PRESSED_JUMP);

            if (JumpIsPressed)
            {
                subComponentProcessor.SetEntity(SetPlayer.ADD_JUMP_FORCE);
                stateProcessor.TransitionTo(typeof(WaitingToJump));
            }
        }

        void CheckWalk()
        {
            float s = subComponentProcessor.GetFloat(PlayerFloat.GET_TARGET_WALK_SPEED);

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