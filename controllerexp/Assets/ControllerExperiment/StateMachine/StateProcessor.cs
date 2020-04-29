using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.States
{
    public class StateProcessor : MonoBehaviour
    {
        [Header("Debug")]
        public List<BaseState> AllStates = new List<BaseState>();
        [Space(10)]
        public BaseState Current = null;

        private void Awake()
        {
            AllStates.Clear();
        }

        void InitState(System.Type type)
        {
            Debug.Log("State initialized: " + type.Name);

            GameObject obj = new GameObject();
            obj.transform.parent = this.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = Quaternion.identity;

            obj.name = type.Name;
            obj.AddComponent(type);

            BaseState newState = obj.GetComponent<BaseState>();
            
            if (!AllStates.Contains(newState))
            {
                AllStates.Add(newState);
            }

            // check if OnEnter is overridden
            System.Type child = newState.GetType();
            newState.Do_OnEnter = OverrideCheck.IsOverridden(child, typeof(BaseState), "OnEnter");
            newState.Do_UpdateState = OverrideCheck.IsOverridden(child, typeof(BaseState), "ProcStateUpdate");

            TransitionTo(newState.GetType());
        }

        public void TransitionTo(System.Type type)
        {
            if (!type.IsSubclassOf(typeof(BaseState)))
            {
                Debug.LogError(type.Name + " is not a state");
            }

            //Debug.Log("Attempting transition to " + type.Name + "..");

            BaseState s = GetState(type);

            if (s == null)
            {
                //Debug.Log(type.Name + " is null. Initiating..");
                InitState(type);
            }
            else
            {
                Debug.Log("Transitioned to: " + type.Name);

                Current = s;

                if (Current.Do_OnEnter)
                {
                    Current.OnEnter();
                }
            }
        }

        BaseState GetState(System.Type type)
        {
            foreach(BaseState s in AllStates)
            {
                if (s.GetType() == type)
                {
                    return s;
                }
            }

            return null;
        }

        public void FixedUpdateState()
        {
            Current.ProcStateFixedUpdate();
        }

        public void UpdateState()
        {
            if (Current.Do_UpdateState)
            {
                Current.ProcStateUpdate();
            }
        }
    }
}