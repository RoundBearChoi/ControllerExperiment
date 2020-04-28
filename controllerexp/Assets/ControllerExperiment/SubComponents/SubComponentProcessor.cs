using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class SubComponentProcessor : MonoBehaviour
    {
        private ControllerEntity m_owner;

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

        public Dictionary<int, Process> ProcDic = new Dictionary<int, Process>();
        public delegate void Process();

        public Dictionary<int, SetEntity> SetFloatDic = new Dictionary<int, SetEntity>();
        public delegate void SetEntity(float f);

        [Header("Debug")]
        public List<SubComponent> SubComponents = new List<SubComponent>();

        private void Awake()
        {
            SubComponents.Clear();

            SubComponent[] arr = this.gameObject.GetComponentsInChildren<SubComponent>();

            foreach(SubComponent s in arr)
            {
                SubComponents.Add(s);
            }
        }
    }
}