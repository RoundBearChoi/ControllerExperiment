using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class RagdollAnimation : SubComponent
    {
        [Header("Attributes")]
        public string DummyRootName;

        [Header("Ragdoll Animation Debug")]
        public List<RagdollAnimator> RagdollAnimators = new List<RagdollAnimator>();

        GameObject Dummy = null;

        private void Start()
        {
            processor.SetDic.Add(SetRagdoll.SET_RAGDOLL_DUMMY, SetDummy);
            processor.SetDic.Add(SetRagdoll.COPY_DUMMY_ANIMATION, CopyAnimation);
            processor.SetDic.Add(SetRagdoll.STOP_ANIMATING, StopAnimating);
            processor.SetDic.Add(SetRagdoll.START_ANIMATING, StartAnimating);
            processor.DelegateGetBool(GetRagdollBool.DUMMY_IS_SET, DummyHasBeenFound);

            FindRagdollSetters();
        }

        void CopyAnimation()
        {
            foreach (RagdollAnimator setter in RagdollAnimators)
            {
                setter.CopyDummyAnimation();
            }
        }

        public void FindRagdollSetters()
        {
            RagdollAnimators.Clear();

            RagdollAnimator[] arr = processor.owner.GetComponentsInChildren<RagdollAnimator>();

            foreach(RagdollAnimator setter in arr)
            {
                RagdollAnimators.Add(setter);
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
            foreach(RagdollAnimator setter in RagdollAnimators)
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

        bool DummyHasBeenFound()
        {
            if (Dummy != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        void StopAnimating()
        {
            foreach(RagdollAnimator a in RagdollAnimators)
            {
                a.DoNotSync = true;
            }
        }

        void StartAnimating()
        {
            foreach (RagdollAnimator a in RagdollAnimators)
            {
                a.DoNotSync = false;
            }
        }
    }
}