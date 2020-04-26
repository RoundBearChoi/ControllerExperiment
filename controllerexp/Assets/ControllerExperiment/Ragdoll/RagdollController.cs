using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : ControllerEntity
    {
        private void FixedUpdate()
        {
            subcomponentProcessor.ProcDic[RagdollProcess.UPDATE_RAGDOLL_POSITIONS]();
        }
    }
}