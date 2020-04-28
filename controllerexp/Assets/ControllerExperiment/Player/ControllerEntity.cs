using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;
using ControllerExperiment.PhysicsState;

namespace ControllerExperiment
{
    public abstract class ControllerEntity : MonoBehaviour
    {
        [Header("Entity Parts")]
        [SerializeField] Rigidbody m_rbody = null;
        [SerializeField] SubComponentProcessor m_SubComponentProcessor = null;
        [SerializeField] StateProcessor m_stateProcessor = null;
        
        public Rigidbody rbody
        {
            get
            {
                if (m_rbody == null)
                {
                    m_rbody = this.gameObject.GetComponent<Rigidbody>();
                }
                return m_rbody;
            }
        }

        public SubComponentProcessor scProcessor
        {
            get
            {
                if (m_SubComponentProcessor == null)
                {
                    m_SubComponentProcessor = this.gameObject.GetComponentInChildren<SubComponentProcessor>();
                }
                return m_SubComponentProcessor;
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