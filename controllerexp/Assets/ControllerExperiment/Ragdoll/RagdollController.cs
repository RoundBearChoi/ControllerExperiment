using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : MonoBehaviour
    {
        [Header("Character Parts")]
        public List<GameObject> RagdollParts = new List<GameObject>();
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();
        public List<CharacterJoint> RightCharacterJoints = new List<CharacterJoint>();
        public List<CharacterJoint> LeftCharacterJoints = new List<CharacterJoint>();

        [Space(10)]
        public GameObject TargetDummy;

        readonly RigidbodyInterpolation DefaultInterpolation = RigidbodyInterpolation.Interpolate;
        readonly CollisionDetectionMode DefaultDetectionMode = CollisionDetectionMode.Continuous;

        [Header("Active Ragdoll")]
        public ConfigurableJoint Hip;

        public GameObject Right_UpperArm;
        public GameObject Right_ForeArm;
        public GameObject Left_UpperArm;
        public GameObject Left_ForeArm;

        [Space(5)]
        [Range(0f, 5000f)]
        public float DriveSpring;
        [Range(0f, 1000f)]
        public float DriveDamper;

        [Space(5)]
        [Range(0f, 5000f)]
        public float AngularDriveSpring;
        [Range(0f, 1000f)]
        public float AngularDriveDamper;

        private void Start()
        {
            RagdollParts.Clear();
            SetupRagdollParts();
        }

        private void FixedUpdate()
        {
            UpdateDriveStrength();
            UpdateAngularDriveStrength();
        }

        public void SetupRagdollParts()
        {
            RagdollParts.Clear();

            foreach (Transform child in this.transform)
            {
                Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();

                foreach (Collider c in colliders)
                {
                    // do not include self
                    if (c.gameObject != this.gameObject)
                    {
                        if (!RagdollParts.Contains(c.gameObject))
                        {
                            c.attachedRigidbody.interpolation = DefaultInterpolation;
                            c.attachedRigidbody.collisionDetectionMode = DefaultDetectionMode;
                            RagdollParts.Add(c.gameObject);
                        }
                    }
                }
            }
        }

        public void SetupCharacterJoints()
        {
            CharacterJoints.Clear();
            RightCharacterJoints.Clear();
            LeftCharacterJoints.Clear();

            CharacterJoint[] joints = this.gameObject.GetComponentsInChildren<CharacterJoint>();

            foreach (CharacterJoint j in joints)
            {
                if (!CharacterJoints.Contains(j))
                {
                    CharacterJoints.Add(j);
                    j.enableProjection = true;
                    j.enableCollision = true;
                }

                AddToRightCharacterJoints(j);
                AddToLeftCharacterJoints(j);
            }
        }

        void AddToRightCharacterJoints(CharacterJoint joint)
        {
            if (joint.name.Contains("right") || joint.name.Contains("Right"))
            {
                if (!RightCharacterJoints.Contains(joint))
                {
                    RightCharacterJoints.Add(joint);
                }
            }
        }

        void AddToLeftCharacterJoints(CharacterJoint joint)
        {
            if (joint.name.Contains("left") || joint.name.Contains("Left"))
            {
                if (!LeftCharacterJoints.Contains(joint))
                {
                    LeftCharacterJoints.Add(joint);
                }
            }
        }

        void UpdateDriveStrength()
        {
            if (UpdateDrive(Hip.xDrive)) { Hip.xDrive = GetDrive(DriveSpring, DriveDamper, Hip.xDrive.maximumForce); }
            if (UpdateDrive(Hip.yDrive)) { Hip.yDrive = GetDrive(DriveSpring, DriveDamper, Hip.yDrive.maximumForce); }
            if (UpdateDrive(Hip.zDrive)) { Hip.zDrive = GetDrive(DriveSpring, DriveDamper, Hip.zDrive.maximumForce); }
        }

        void UpdateAngularDriveStrength()
        {
            if (UpdateDrive(Hip.angularXDrive)) { Hip.angularXDrive = GetDrive(AngularDriveSpring, AngularDriveDamper, Hip.angularXDrive.maximumForce); }
            if (UpdateDrive(Hip.angularYZDrive)) { Hip.angularYZDrive = GetDrive(AngularDriveSpring, AngularDriveDamper, Hip.angularYZDrive.maximumForce); }
        }

        bool UpdateDrive(JointDrive drive)
        {
            if (!drive.positionSpring.Equals(DriveSpring) || !drive.positionDamper.Equals(DriveDamper))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        JointDrive GetDrive(float spring, float damper, float maxforce)
        {
            JointDrive newdrive = new JointDrive();
            newdrive.positionSpring = spring;
            newdrive.positionDamper = damper;
            newdrive.maximumForce = maxforce;
            return newdrive;
        }
    }
}