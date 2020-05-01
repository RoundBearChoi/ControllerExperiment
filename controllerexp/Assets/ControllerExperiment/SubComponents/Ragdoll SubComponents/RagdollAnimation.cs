using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class RagdollAnimation : SubComponent
    {
        [Header("Attributes")]
        [SerializeField] string DummyRootName;

        [Header("Ragdoll Animation Debug")]
        [SerializeField] List<RagdollMover> RagdollMovers = new List<RagdollMover>();
        [Space(5)]
        [SerializeField] Rigidbody HipRigidbody;
        [SerializeField] ConfigurableJoint HipJoint;
        [SerializeField] Rigidbody RootPivot;
        [Space(5)]
        [SerializeField] GameObject Dummy = null;

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

            HipRigidbody = GetHip();
            HipJoint = HipRigidbody.GetComponent<ConfigurableJoint>();
            RootPivot = HipJoint.connectedBody;
        }

        Rigidbody GetHip()
        {
            ConfigurableJoint[] arr = processor.owner.GetComponentsInChildren<ConfigurableJoint>();

            foreach(ConfigurableJoint j in arr)
            {
                if (j.name.Contains("hip") || j.name.Contains("Hip"))
                {
                    return j.GetComponent<Rigidbody>();
                }
            }

            return null;
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

        void JointDefaultSettings(ConfigurableJoint joint)
        {
            joint.enableCollision = true;
            joint.enablePreprocessing = false;
        }

        void RigidBodyDefaultSettings(Rigidbody rbody)
        {
            rbody.interpolation = RigidbodyInterpolation.Interpolate;
            rbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        void StopAnimating()
        {
            foreach(RagdollMover a in RagdollMovers)
            {
                a.DoNotSync = true;

                JointDefaultSettings(a.myJoint);
                RigidBodyDefaultSettings(a.myRigidBody);
                
                JointUpdater.UpdateAngularDrive(a.myJoint, 0f, 0f);
                JointUpdater.UpdateTargetRotation(a.myJoint, Vector3.zero);
            }

            JointDefaultSettings(HipJoint);
            RigidBodyDefaultSettings(HipRigidbody);
        }

        void StartAnimating()
        {
            foreach (RagdollMover a in RagdollMovers)
            {
                a.DoNotSync = false;

                JointDefaultSettings(a.myJoint);
                RigidBodyDefaultSettings(a.myRigidBody);

                JointUpdater.UpdateAngularDrive(a.myJoint, 1000f, 0f);
            }

            JointDefaultSettings(HipJoint);
            RigidBodyDefaultSettings(HipRigidbody);
        }

        void AlignRootPivotToRootObj()
        {
            RootPivot.MoveRotation(Quaternion.Euler(0, this.transform.root.gameObject.transform.rotation.y, 0f));
        }
    }
}