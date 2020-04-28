using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public class StateProcessor : MonoBehaviour
    {
        [Header("Debug")]
        public List<PhysicsState> AllStates = new List<PhysicsState>();
        [Space(10)]
        public PhysicsState Current = null;

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

            PhysicsState newState = obj.GetComponent<PhysicsState>();
            
            if (!AllStates.Contains(newState))
            {
                AllStates.Add(newState);
            }

            TransitionTo(newState.GetType());
        }

        public void TransitionTo(System.Type type)
        {
            if (!type.IsSubclassOf(typeof(PhysicsState)))
            {
                Debug.LogError(type.Name + " is not a PhysicsState..");
            }

            //Debug.Log("Attempting transition to " + type.Name + "..");

            PhysicsState s = GetState(type);

            if (s == null)
            {
                //Debug.Log(type.Name + " is null. Initiating..");
                InitState(type);
            }
            else
            {
                Debug.Log("Transitioned to: " + type.Name);
                Current = s;
                Current.OnEnter();
            }
        }

        PhysicsState GetState(System.Type type)
        {
            foreach(PhysicsState s in AllStates)
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
            Current.ProcStateUpdate();
        }
    }
}