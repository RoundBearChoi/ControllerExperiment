using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class RagdollAnimation : SubComponent
    {
        [Header("Attributes")]
        public string DummyRootName;

        [Header("Ragdoll Animation Debug")]
        public List<RagdollAnimator> RagdollPartSetters = new List<RagdollAnimator>();

        GameObject Dummy;

        private void Start()
        {
            processor.ProcDic.Add(RagdollProcess.SET_RAGDOLL_DUMMY, SetDummy);

            FindRagdollSetters();
        }

        public override void OnFixedUpdate()
        {
            foreach (RagdollAnimator setter in RagdollPartSetters)
            {
                setter.CopyDummyAnimation();
            }
        }

        public void FindRagdollSetters()
        {
            RagdollPartSetters.Clear();

            RagdollAnimator[] arr = processor.owner.GetComponentsInChildren<RagdollAnimator>();

            foreach(RagdollAnimator setter in arr)
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
            foreach(RagdollAnimator setter in RagdollPartSetters)
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