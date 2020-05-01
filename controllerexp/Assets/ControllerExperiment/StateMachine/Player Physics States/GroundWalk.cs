using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.States.Player
{
    public class GroundWalk : BaseState
    {
        [Header("Idle Debug")]
        [SerializeField] readonly float GroundSpeed = 1.75f;

        public override void OnEnter()
        {
            subComponentProcessor.SetFloat(PlayerFloat.SET_TARGET_WALK_SPEED, GroundSpeed);
            subComponentProcessor.SetEntity(SetPlayer.PLAY_ANIMATION_WALK);
        }

        public override void ProcStateFixedUpdate()
        {
            subComponentProcessor.SetEntity(SetPlayer.SET_WALK_DIRECTION);
            subComponentProcessor.SetEntity(SetPlayer.ROTATE_TO_TARGET_ANGLE);
            subComponentProcessor.SetEntity(SetPlayer.WALK_TO_TARGET_DIRECTION);
            subComponentProcessor.SetEntity(SetPlayer.CANCEL_VERTICAL_VELOCITY);

            if (JumpCheck.Jump(subComponentProcessor))
            {
                subComponentProcessor.SetEntity(SetPlayer.ADD_JUMP_FORCE);
                stateProcessor.TransitionTo(typeof(WaitingToJump));
            }

            float s = subComponentProcessor.GetFloat(PlayerFloat.GET_TARGET_WALK_SPEED);
            if (s <= 0.001f)
            {
                stateProcessor.TransitionTo(typeof(GroundIdle));
            }
        }
    }
}