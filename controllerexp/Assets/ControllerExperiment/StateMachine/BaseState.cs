using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States
{
    public abstract class BaseState: MonoBehaviour
    {
        [Header("State Properties")]
        public bool Do_OnEnter;
        public bool Do_UpdateState;

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
            throw new System.NotImplementedException();
        }

        public virtual void ProcStateUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}