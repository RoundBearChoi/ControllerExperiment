using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class RagdollAnimation : SubComponent
    {
        [Header("Attributes")]
        public string DummyRootName;

        [Header("Debug")]
        public List<TempRagdollSetter> RagdollPartSetters = new List<TempRagdollSetter>();

        GameObject Dummy;

        private void Start()
        {
            processor.ProcDic.Add(RagdollProcess.SET_RAGDOLL_DUMMY, SetDummy);

            FindRagdollSetters();
        }

        public void FindRagdollSetters()
        {
            RagdollPartSetters.Clear();

            TempRagdollSetter[] arr = processor.owner.GetComponentsInChildren<TempRagdollSetter>();

            foreach(TempRagdollSetter setter in arr)
            {
                RagdollPartSetters.Add(setter);
            }
        }

        void SetDummy()
        {
            Dummy = GameObject.Find(DummyRootName);

            if (Dummy != null)
            {
                SetMirrorParts();
            }
        }

        void SetMirrorParts()
        {
            foreach(TempRagdollSetter setter in RagdollPartSetters)
            {
                Transform[] arr = Dummy.GetComponentsInChildren<Transform>();
                foreach(Transform t in arr)
                {
                    if (setter.name.Equals(t.name))
                    {
                        setter.SetMirrorJoint(t.gameObject);
                        setter.SetAnchors(); //must set anchors on start
                    }
                }
            }
        }
    }
}