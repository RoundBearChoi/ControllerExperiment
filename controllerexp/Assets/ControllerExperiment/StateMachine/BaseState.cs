using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States
{
    public abstract class BaseState: MonoBehaviour
    {
        public ControllerEntity owner
        {
            get
            {
                if (m_owner == null)
                {
                    m_owner = this.gameObject.GetComponentInParent<ControllerEntity>();
                }
                return m_owner;
            }
        }

        ControllerEntity m_owner = null;

        public abstract void ProcStateFixedUpdate();

        public virtual void OnEnter()
        {
            // called once on beginning of every transition (optional)
        }

        public virtual void ProcStateUpdate()
        {
            // called every frame (optional)
        }
    }
}