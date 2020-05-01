using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;
using ControllerExperiment.States;

namespace ControllerExperiment
{
    public abstract class ControllerEntity : MonoBehaviour
    {
        [Header("Controller Entity Debug")]
        [SerializeField] Rigidbody mRigidBody = null;
        [SerializeField] SubComponentProcessor m_SubComponentProcessor = null;
        [Space(5)]
        [SerializeField] List<StateProcessor> StateProcessorsList = new List<StateProcessor>();

        Dictionary<StateProcessorType, StateProcessor> StateProcessorDic = new Dictionary<StateProcessorType, StateProcessor>();

        private void Awake()
        {
            StateProcessorsList.Clear();
            StateProcessorDic.Clear();
        }

        public Rigidbody rbody
        {
            get
            {
                if (mRigidBody == null)
                {
                    mRigidBody = this.gameObject.GetComponent<Rigidbody>();
                }
                return mRigidBody;
            }
        }

        public SubComponentProcessor subComponentProcessor
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

        protected StateProcessor GetStateProcessor(StateProcessorType type)
        {
            if (StateProcessorDic.Count == 0)
            {
                StateProcessor[] arr = this.gameObject.GetComponentsInChildren<StateProcessor>();

                StateProcessorsList.AddRange(arr);

                foreach (StateProcessor p in arr)
                {
                    StateProcessorDic.Add(p.m_StateType, p);
                }
            }

            return StateProcessorDic[type];
        }

        protected void UpdateStateProcessors()
        {
            foreach (StateProcessor p in StateProcessorsList)
            {
                p.UpdateState();
            }
        }

        protected void FixedUpdateStateProcessors()
        {
            foreach (StateProcessor p in StateProcessorsList)
            {
                p.FixedUpdateState();
            }
        }
    }
}