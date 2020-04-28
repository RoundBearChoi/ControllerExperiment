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

        public Dictionary<int, Process> SetDic = new Dictionary<int, Process>();
        public delegate void Process();

        public Dictionary<int, SetEntityFloat> SetFloatDic = new Dictionary<int, SetEntityFloat>();
        public delegate void SetEntityFloat(float f);

        public Dictionary<int, SetEntityBool> SetBoolDic = new Dictionary<int, SetEntityBool>();
        public delegate void SetEntityBool(bool b);

        public Dictionary<int, GetBool> GetBoolDic = new Dictionary<int, GetBool>();
        public delegate bool GetBool();

        public Dictionary<int, GetInt> GetIntDic = new Dictionary<int, GetInt>();
        public delegate int GetInt();

        [Header("Debug")]
        public List<SubComponent> SubComponents = new List<SubComponent>();

        private void Awake()
        {
            SubComponents.Clear();

            SubComponent[] arr = this.gameObject.GetComponentsInChildren<SubComponent>();

            foreach(SubComponent s in arr)
            {
                SubComponents.Add(s);

                // check whether the subcomponent needs to be updated/fixedupdated
                System.Type child = s.GetType();
                s.DoFixedUpdate = OverrideCheck.IsOverridden(child, typeof(SubComponent), "OnFixedUpdate");
                s.DoUpdate = OverrideCheck.IsOverridden(child, typeof(SubComponent), "OnUpdate");
            }
        }

        public void FixedUpdateSubComponents()
        {
            foreach(SubComponent s in SubComponents)
            {
                if (s.DoFixedUpdate)
                {
                    s.OnFixedUpdate();
                }
            }
        }

        public void UpdateSubComponents()
        {
            foreach (SubComponent s in SubComponents)
            {
                if (s.DoUpdate)
                {
                    s.OnUpdate();
                }
            }
        }

        
    }
}