using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : ControllerEntity
    {
        private void Start()
        {
            scProcessor.ProcDic[RagdollProcess.SET_RAGDOLL_DUMMY]();
        }

        private void FixedUpdate()
        {
            scProcessor.FixedUpdateSubComponents();
        }
    }
}