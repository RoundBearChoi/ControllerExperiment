using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents.Ragdoll;
using ControllerExperiment.States.Ragdoll;

namespace ControllerExperiment
{
    public enum SelectedRagdoll
    {
        COPY_DUMMY_ANIMATION,
        LIFELESS_RAGDOLL,
    }

    public class RagdollController : ControllerEntity
    {
        [Header("Attributes")]
        public SelectedRagdoll m_DesiredState;

        private void Start()
        {
            stateProcessor.TransitionTo(typeof(RagdollStart));
            subComponentProcessor.SetDic[SetRagdoll.SET_RAGDOLL_DUMMY]();
            subComponentProcessor.DelegateGetInt(GetRagdollInt.DESIRED_RAGDOLL_STATE, GetDesiredState);
        }

        private void Update()
        {
            stateProcessor.UpdateState();
        }

        private void FixedUpdate()
        {
            stateProcessor.FixedUpdateState();
            subComponentProcessor.FixedUpdateSubComponents();
        }

        int GetDesiredState()
        {
            return (int)m_DesiredState;
        }
    }
}