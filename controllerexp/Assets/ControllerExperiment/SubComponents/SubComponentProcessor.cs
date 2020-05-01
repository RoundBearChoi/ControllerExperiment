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

        Dictionary<int, SetEntityDelegate> SetEntityDic = new Dictionary<int, SetEntityDelegate>();
        public delegate void SetEntityDelegate();

        Dictionary<int, SetFloatDelegate> SetFloatDic = new Dictionary<int, SetFloatDelegate>();
        public delegate void SetFloatDelegate(float f);

        Dictionary<int, GetFloatDelegate> GetFloatDic = new Dictionary<int, GetFloatDelegate>();
        public delegate float GetFloatDelegate();

        Dictionary<int, SetBoolDelegate> SetBoolDic = new Dictionary<int, SetBoolDelegate>();
        public delegate void SetBoolDelegate(bool b);

        Dictionary<int, GetBoolDelegate> GetBoolDic = new Dictionary<int, GetBoolDelegate>();
        public delegate bool GetBoolDelegate();

        Dictionary<int, GetIntDelegate> GetIntDic = new Dictionary<int, GetIntDelegate>();
        public delegate int GetIntDelegate();

        [Header("SubComponent Processor Debug")]
        public List<BaseSubComponent> SubComponents = new List<BaseSubComponent>();

        private void Awake()
        {
            SubComponents.Clear();

            BaseSubComponent[] arr = this.gameObject.GetComponentsInChildren<BaseSubComponent>();

            foreach(BaseSubComponent s in arr)
            {
                SubComponents.Add(s);

                // check whether the subcomponent needs to be updated/fixedupdated
                System.Type child = s.GetType();
                s.DoFixedUpdate = OverrideCheck.IsOverridden(child, typeof(BaseSubComponent), "OnFixedUpdate");
                s.DoUpdate = OverrideCheck.IsOverridden(child, typeof(BaseSubComponent), "OnUpdate");
            }
        }

        public void FixedUpdateSubComponents()
        {
            foreach(BaseSubComponent s in SubComponents)
            {
                if (s.DoFixedUpdate)
                {
                    s.OnFixedUpdate();
                }
            }
        }

        public void UpdateSubComponents()
        {
            foreach (BaseSubComponent s in SubComponents)
            {
                if (s.DoUpdate)
                {
                    s.OnUpdate();
                }
            }
        }

        public void SetEntity(int key)
        {
            if (SetEntityDic.ContainsKey(key))
            {
                SetEntityDic[key]();
            }
            else
            {
                Debug.LogError("Function not found");
            }
        }

        public void DelegateSetEntity(int key, SetEntityDelegate del)
        {
            SetEntityDic.Add(key, del);
        }

        public void SetFloat(int key, float f)
        {
            if (SetFloatDic.ContainsKey(key))
            {
                SetFloatDic[key](f);
            }
            else
            {
                Debug.LogError("SetFloat function not found");
            }
        }

        public void DelegateSetFloat(int key, SetFloatDelegate del)
        {
            SetFloatDic.Add(key, del);
        }

        public float GetFloat(int key)
        {
            if (GetFloatDic.ContainsKey(key))
            {
                return GetFloatDic[key]();
            }
            else
            {
                Debug.LogError("GetFloat function not found");
                return 0f;
            }
        }

        public void DelegateGetFloat(int key, GetFloatDelegate del)
        {
            GetFloatDic.Add(key, del);
        }

        public void SetBool(int key, bool b)
        {
            if (SetBoolDic.ContainsKey(key))
            {
                SetBoolDic[key](b);
            }
            else
            {
                Debug.LogError("SetBool function not found");
            }
        }

        public void DelegateSetBool(int key, SetBoolDelegate del)
        {
            SetBoolDic.Add(key, del);
        }

        public bool GetBool(int key)
        {
            if (GetBoolDic.ContainsKey(key))
            {
                return GetBoolDic[key]();
            }
            else
            {
                Debug.LogError("GetBool function not found");
                return false;
            }
        }

        public void DelegateGetBool(int key, GetBoolDelegate del)
        {
            GetBoolDic.Add(key, del);
        }

        public int GetInt(int key)
        {
            if (GetIntDic.ContainsKey(key))
            {
                return GetIntDic[key]();
            }
            else
            {
                Debug.LogError("GetInt function not found");
                return 0;
            }
        }

        public void DelegateGetInt(int key, GetIntDelegate del)
        {
            GetIntDic.Add(key, del);
        }
    }
}