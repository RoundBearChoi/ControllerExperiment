using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States
{
    public abstract class BaseState: MonoBehaviour
    {
        [Header("Base Debug")]
        public bool Do_OnEnter;
        public bool Do_UpdateState;
        public StateProcessor stateProcessor;

        public abstract void ProcStateFixedUpdate();

        public virtual void OnEnter()
        {
            throw new System.NotImplementedException();
        }

        public virtual void ProcStateUpdate()
        {
            throw new System.NotImplementedException();
        }

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

        public SubComponents.SubComponentProcessor subComponentProcessor
        {
            get
            {
                return owner.subComponentProcessor;
            }
        }
    }
}