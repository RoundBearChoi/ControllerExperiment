using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class RagdollMovement : SubComponent
    {
        [Header("Attributes")]
        public float DriveSpring;
        public float DriveDamper;
        public float DriveMaxForce;
        public RigidbodyInterpolation interpolate;
        public CollisionDetectionMode collision;
        public bool ConnectedBodiesCollision;
        public bool MirrorRendererOn;

        [Header("Debug")]
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();
        [Space(5)]
        public GameObject TargetRootMirror;
        public List<ConfigurableJoint> ConfigurableJoints = new List<ConfigurableJoint>();
        public Dictionary<GameObject, GameObject> TargetMirrorDic = new Dictionary<GameObject, GameObject>();

        private void Start()
        {
            processor.ProcDic.Add(RagdollProcess.UPDATE_RAGDOLL_POSITIONS, UpdateRagdollPositions);

            FindCharacterJoints();
            SetCharacterJointAttributes();
            FindConfigurableJointMirrors();

            TurnOffMirrorRenderer();
        }

        public void FindCharacterJoints()
        {
            CharacterJoints.Clear();

            CharacterJoint[] joints = processor.owner.gameObject.GetComponentsInChildren<CharacterJoint>();

            foreach (CharacterJoint j in joints)
            {
                if (!CharacterJoints.Contains(j))
                {
                    CharacterJoints.Add(j);
                    j.enableProjection = true;
                    j.enableCollision = true;
                }
            }
        }

        public void SetCharacterJointAttributes()
        {
            foreach(CharacterJoint j in CharacterJoints)
            {
                Rigidbody body = j.GetComponent<Rigidbody>();

                body.interpolation = interpolate;
                body.collisionDetectionMode = collision;
                j.enableCollision = ConnectedBodiesCollision;
            }
        }

        public void FindConfigurableJointMirrors()
        {
            TargetMirrorDic.Clear();
            ConfigurableJoints.Clear();

            ConfigurableJoint[] myConfigurables = processor.owner.gameObject.GetComponentsInChildren<ConfigurableJoint>();
            Transform[] all = TargetRootMirror.gameObject.GetComponentsInChildren<Transform>();

            foreach(ConfigurableJoint j in myConfigurables)
            {
                TargetMirrorDic.Add(j.gameObject, null);
                ConfigurableJoints.Add(j);

                //temporary..
                CharacterJoint charjoint = j.gameObject.GetComponent<CharacterJoint>();
                if (charjoint != null)
                {
                    Destroy(charjoint);
                }
            }

            foreach(Transform t in all)
            {
                foreach(ConfigurableJoint c in myConfigurables)
                {
                    if (t.gameObject.name.Equals(c.gameObject.name))
                    {
                        TargetMirrorDic[c.gameObject] = t.gameObject;
                    }
                }
            }
        }

        void UpdateRagdollPositions()
        {
            processor.owner.rbody.MovePosition(TargetRootMirror.transform.position);
            processor.owner.rbody.MoveRotation(TargetRootMirror.transform.rotation);

            foreach(ConfigurableJoint j in ConfigurableJoints)
            {
                UpdateDrive(j);

                if (TargetMirrorDic.ContainsKey(j.gameObject))
                {
                    j.targetRotation = TargetMirrorDic[j.gameObject].transform.localRotation;
                }
            }
        }

        void UpdateDrive(ConfigurableJoint joint)
        {
            if (MustUpdateDrive(joint.xDrive))
            {
                joint.xDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
                joint.yDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
                joint.zDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
            }
        }

        bool MustUpdateDrive(JointDrive drive)
        {
            if (!drive.positionSpring.Equals(DriveSpring) ||
                !drive.positionDamper.Equals(DriveDamper) ||
                !drive.maximumForce.Equals(DriveMaxForce))
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

        void TurnOffMirrorRenderer()
        {
            if (TargetRootMirror != null)
            {
                Renderer[] arr = TargetRootMirror.GetComponentsInChildren<Renderer>();

                foreach (Renderer r in arr)
                {
                    r.enabled = MirrorRendererOn;
                }
            }
        }
    }
}