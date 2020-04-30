using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;
using ControllerExperiment.States.Ragdoll;

namespace ControllerExperiment
{
    public enum RagdollAnimationState
    {
        COPY_DUMMY_ANIMATION,
        LIFELESS_RAGDOLL,
    }

    public enum RagdollTransformState
    {
        FOLLOW_DUMMY_POSITION,
        DONT_FOLLOW_DUMMY_POSITION,
    }

    public class RagdollController : ControllerEntity
    {
        [Header("Attributes")]
        public RagdollAnimationState m_SelectedAnimationState;
        public RagdollTransformState m_SelectedTransformState;

        private void Start()
        {
            // must init ragdoll states
            GetStateProcessor(STATE.RAGDOLL_ANIMATION).TransitionTo(typeof(RagdollAnimationStart));
            GetStateProcessor(STATE.RAGDOLL_TRANSFORM).TransitionTo(typeof(RagdollTransformStart));

            subComponentProcessor.SetEntity(SetRagdoll.SET_RAGDOLL_DUMMY);
            subComponentProcessor.DelegateGetInt(RagdollInt.RAGDOLL_ANIMATION_STATE, GetSelectedAnimationState);
            subComponentProcessor.DelegateGetInt(RagdollInt.RAGDOLL_TRANSFORM_STATE, GetSelectedTransformState);
        }

        private void Update()
        {
            UpdateStateProcessors();
        }

        private void FixedUpdate()
        {
            FixedUpdateStateProcessors();
            subComponentProcessor.FixedUpdateSubComponents();
        }

        int GetSelectedAnimationState()
        {
            return (int)m_SelectedAnimationState;
        }

        int GetSelectedTransformState()
        {
            return (int)m_SelectedTransformState;
        }
    }
}