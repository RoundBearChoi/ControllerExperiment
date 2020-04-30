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
        public List<RagdollMover> RagdollMovers = new List<RagdollMover>();
        [Space(5)]
        public Rigidbody HipRigidbody;
        public ConfigurableJoint HipJoint;
        public Rigidbody RootPivot;

        GameObject Dummy = null;

        private void Start()
        {
            processor.DelegateSetEntity(SetRagdoll.SET_RAGDOLL_DUMMY, SetDummy);
            processor.DelegateSetEntity(SetRagdoll.COPY_DUMMY_ANIMATION, CopyAnimation);
            processor.DelegateSetEntity(SetRagdoll.STOP_ANIMATING, StopAnimating);
            processor.DelegateSetEntity(SetRagdoll.START_ANIMATING, StartAnimating);

            FindRagdollSetters();
        }

        void CopyAnimation()
        {
            Debug.DrawLine(HipRigidbody.position, RootPivot.position, Color.yellow);

            foreach (RagdollMover setter in RagdollMovers)
            {
                setter.CopyDummyAnimation();
            }
        }

        public void FindRagdollSetters()
        {
            RagdollMovers.Clear();

            RagdollMover[] arr = processor.owner.GetComponentsInChildren<RagdollMover>();
            RagdollMovers.AddRange(arr);

            HipRigidbody = GetHip(RagdollMovers[0].myJoint);
            HipJoint = HipRigidbody.GetComponent<ConfigurableJoint>();
            RootPivot = HipJoint.connectedBody;
        }

        Rigidbody GetHip(ConfigurableJoint joint)
        {
            // get parent joint
            Rigidbody parentRigid = joint.connectedBody;
            ConfigurableJoint parentJoint = parentRigid.GetComponent<ConfigurableJoint>();

            // if parent joint doesn't have configurable joint, this joint is a hip
            if (parentJoint == null)
            {
                return joint.GetComponent<Rigidbody>();
            }
            else
            {
                return GetHip(parentRigid.GetComponent<ConfigurableJoint>());
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
            foreach(RagdollMover setter in RagdollMovers)
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

        void StopAnimating()
        {
            foreach(RagdollMover a in RagdollMovers)
            {
                a.DoNotSync = true;
            }
        }

        void StartAnimating()
        {
            foreach (RagdollMover a in RagdollMovers)
            {
                a.DoNotSync = false;
            }
        }
    }
}