using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public abstract class PhysicsState: MonoBehaviour
    {
        protected PlayerController control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<PlayerController>();
        }
                
        public abstract void ProcStateFixedUpdate();

        public virtual void OnEnter()
        {

        }

        public virtual void ProcStateUpdate()
        {

        }
    }
}