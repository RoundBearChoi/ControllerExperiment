using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class RagdollAnimation : BaseSubComponent
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

            HipRigidbody = RagdollPartFinder.GetHip(processor.owner);
            HipJoint = HipRigidbody.GetComponent<ConfigurableJoint>();
            RootPivot = HipJoint.connectedBody;
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

        void CharacterBodyDefaultSettings(Rigidbody rbody)
        {
            rbody.interpolation = RigidbodyInterpolation.Interpolate;
            rbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rbody.isKinematic = false;
            rbody.useGravity = true;
        }

        void RootBodyDefaultSettings(Rigidbody rbody)
        {
            rbody.interpolation = RigidbodyInterpolation.Interpolate;
            rbody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
            rbody.isKinematic = true;
            rbody.useGravity = false;
        }

        void StopAnimating()
        {
            foreach(RagdollMover a in RagdollMovers)
            {
                a.DoNotSync = true;

                JointDefaultSettings(a.myJoint);
                CharacterBodyDefaultSettings(a.myRigidBody);
                
                JointUpdater.UpdateAngularDrive(a.myJoint, 0f, 0f);
                JointUpdater.UpdateTargetRotation(a.myJoint, Vector3.zero);
            }

            JointDefaultSettings(HipJoint);
            CharacterBodyDefaultSettings(HipRigidbody);

            RootBodyDefaultSettings(RootPivot);
        }

        void StartAnimating()
        {
            foreach (RagdollMover a in RagdollMovers)
            {
                a.DoNotSync = false;

                JointDefaultSettings(a.myJoint);
                CharacterBodyDefaultSettings(a.myRigidBody);

                JointUpdater.UpdateAngularDrive(a.myJoint, 1000f, 0f);
            }

            JointDefaultSettings(HipJoint);
            CharacterBodyDefaultSettings(HipRigidbody);

            RootBodyDefaultSettings(RootPivot);
        }
    }
}