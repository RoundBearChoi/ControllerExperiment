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

        protected ControllerEntity owner
        {
            get
            {
                if (mOwner == null)
                {
                    mOwner = this.gameObject.GetComponentInParent<ControllerEntity>();
                }
                return mOwner;
            }
        }

        ControllerEntity mOwner = null;

        protected SubComponents.SubComponentProcessor subComponentProcessor
        {
            get
            {
                return owner.subComponentProcessor;
            }
        }
    }
}