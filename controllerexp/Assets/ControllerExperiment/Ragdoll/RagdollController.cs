using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : ControllerEntity
    {
        [Header("Found on Awake")]
        public SubComponentProcessor RagdollComponents;

        private void Awake()
        {
            RagdollComponents = this.gameObject.GetComponentInChildren<SubComponentProcessor>();
        }
    }
}