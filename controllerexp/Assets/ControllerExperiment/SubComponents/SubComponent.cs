using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public abstract class SubComponent : MonoBehaviour
    {
        [Header("Found on Awake")]
        public SubComponentProcessor processor;

        private void Awake()
        {
            processor = this.gameObject.GetComponentInParent<SubComponentProcessor>();
        }

        public virtual void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        public virtual void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}