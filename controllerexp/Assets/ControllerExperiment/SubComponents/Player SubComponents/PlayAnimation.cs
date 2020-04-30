using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;
using ControllerExperiment.Animations;

namespace ControllerExperiment.SubComponents.Player
{
    public class PlayAnimation : SubComponent
    {
        [Header("Play Animation Attributes")]
        [SerializeField] string TargetAnimationObjectName;

        private void Start()
        {
            processor.DelegateSetEntity(SetPlayer.PLAY_ANIMATION_IDLE, Play_Idle);
            processor.DelegateSetEntity(SetPlayer.PLAY_ANIMATION_WALK, Play_Walk);
        }

        void Play_Idle()
        {
            AnimationControl.Play(TargetAnimationObjectName, AnimationNames.Idle, 0f);
        }

        void Play_Walk()
        {
            AnimationControl.Play(TargetAnimationObjectName, AnimationNames.Walking, 0f);
        }
    }
}