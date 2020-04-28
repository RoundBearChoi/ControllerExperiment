using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;
using ControllerExperiment.States;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : ControllerEntity
    {
        private void Start()
        {
            stateProcessor.TransitionTo(typeof(RagdollStart));
            subComponentProcessor.SetDic[SetRagdoll.SET_RAGDOLL_DUMMY]();
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
    }
}