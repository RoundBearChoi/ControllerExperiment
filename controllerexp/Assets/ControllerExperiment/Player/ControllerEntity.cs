using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;
using ControllerExperiment.PhysicsState;

namespace ControllerExperiment
{
    public abstract class ControllerEntity : MonoBehaviour
    {
        private Rigidbody m_rigidBody;
        private SubComponentProcessor m_subcomponentProcessor;
        private StateProcessor m_stateProcessor;

        public Rigidbody rbody
        {
            get
            {
                if (m_rigidBody == null)
                {
                    m_rigidBody = this.gameObject.GetComponent<Rigidbody>();
                }
                return m_rigidBody;
            }
        }

        public SubComponentProcessor subcomponentProcessor
        {
            get
            {
                if (m_subcomponentProcessor == null)
                {
                    m_subcomponentProcessor = this.gameObject.GetComponentInChildren<SubComponentProcessor>();
                }
                return m_subcomponentProcessor;
            }
        }

        public StateProcessor stateProcessor
        {
            get
            {
                if (m_stateProcessor == null)
                {
                    m_stateProcessor = this.gameObject.GetComponentInChildren<StateProcessor>();
                }
                return m_stateProcessor;
            }
        }
    }
}