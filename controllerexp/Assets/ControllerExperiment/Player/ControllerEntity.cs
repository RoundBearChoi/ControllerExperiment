﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;
using ControllerExperiment.States;

namespace ControllerExperiment
{
    public abstract class ControllerEntity : MonoBehaviour
    {
        [Header("Controller Entity Debug")]
        public Rigidbody m_rbody = null;
        [SerializeField] SubComponentProcessor m_SubComponentProcessor = null;
        [Space(5)]
        [SerializeField] List<StateProcessor> StateProcessorsList = new List<StateProcessor>();

        Dictionary<STATE, StateProcessor> StateProcessorDic = new Dictionary<STATE, StateProcessor>();

        private void Awake()
        {
            StateProcessorsList.Clear();
            StateProcessorDic.Clear();
        }

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

        protected StateProcessor GetStateProcessor(STATE type)
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