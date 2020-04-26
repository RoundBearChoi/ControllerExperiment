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

            if (TargetRootMirror != null)
            {
                //temporary..
                //AddConfigurables();

                Transform[] all = TargetRootMirror.gameObject.GetComponentsInChildren<Transform>();
                ConfigurableJoint[] myConfigurables = processor.owner.gameObject.GetComponentsInChildren<ConfigurableJoint>();

                foreach (ConfigurableJoint j in myConfigurables)
                {
                    TargetMirrorDic.Add(j.gameObject, null);
                    ConfigurableJoints.Add(j);

                    //temporary..
                    //ConnectBody(j);
                }

                //temporary..
                //DestroyCharacterJoints(myConfigurables);

                foreach (Transform t in all)
                {
                    foreach (ConfigurableJoint c in myConfigurables)
                    {
                        if (t.gameObject.name.Equals(c.gameObject.name))
                        {
                            TargetMirrorDic[c.gameObject] = t.gameObject;
                        }
                    }
                }
            }
        }

        void AddConfigurables()
        {
            foreach(CharacterJoint j in CharacterJoints)
            {
                if (j.gameObject.name.Contains("Arm"))
                {
                    ConfigurableJoint conf = j.gameObject.GetComponent<ConfigurableJoint>();

                    if (conf == null)
                    {
                        j.gameObject.AddComponent<ConfigurableJoint>();
                        ConfigurableJoint c = j.gameObject.GetComponent<ConfigurableJoint>();
                        c.configuredInWorldSpace = true;
                    }
                }
            }
        }

        void ConnectBody(ConfigurableJoint joint)
        {
            CharacterJoint charjoint = joint.gameObject.GetComponent<CharacterJoint>();
            if (charjoint != null)
            {
                joint.connectedBody = charjoint.connectedBody;
            }
        }

        void DestroyCharacterJoints(ConfigurableJoint[] arr)
        {
            foreach (ConfigurableJoint conf in arr)
            {
                CharacterJoint charjoint = conf.gameObject.GetComponent<CharacterJoint>();
                if (charjoint != null)
                {
                    Destroy(charjoint);
                }
            }
        }

        void UpdateRagdollPositions()
        {
            //processor.owner.rbody.MovePosition(TargetRootMirror.transform.position);
            //processor.owner.rbody.MoveRotation(TargetRootMirror.transform.rotation);

            foreach(ConfigurableJoint j in ConfigurableJoints)
            {
                UpdateDrive(j);
                UpdateAngularDrive(j);

                if (TargetMirrorDic.ContainsKey(j.gameObject))
                {
                    GameObject connected = j.connectedBody.gameObject;

                    if (TargetMirrorDic.ContainsKey(connected))
                    {
                        j.targetPosition = TargetMirrorDic[j.gameObject].transform.position - TargetMirrorDic[connected].transform.position;
                    }
                    else
                    {
                        SetTargetPosition(j, TargetRootMirror.transform.position);
                    }
                }
            }
        }

        void SetTargetPosition(ConfigurableJoint myJoint, Vector3 virtualConnectedBodyPosition)
        {
            Vector3 anchorPosition = myJoint.connectedBody.transform.position + myJoint.connectedAnchor;
            Vector3 bodyPosition = myJoint.transform.position;
            Debug.DrawLine(bodyPosition, anchorPosition, Color.red, 0.5f);

            Vector3 vConnectedBody = virtualConnectedBodyPosition;
            Vector3 vAnchorPosition = vConnectedBody + myJoint.connectedAnchor;
            Vector3 virtualBodyPosition = TargetMirrorDic[myJoint.gameObject].transform.position;
            Debug.DrawLine(virtualBodyPosition, vAnchorPosition, Color.green, 0.5f);

            myJoint.targetPosition = virtualBodyPosition - vAnchorPosition;
        }

        void UpdateDrive(ConfigurableJoint joint)
        {
            if (MustUpdateDrive(joint.xDrive) ||
                MustUpdateDrive(joint.yDrive) ||
                MustUpdateDrive(joint.zDrive))
            {
                joint.xDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
                joint.yDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
                joint.zDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
            }
        }

        void UpdateAngularDrive(ConfigurableJoint joint)
        {
            if (MustUpdateDrive(joint.angularXDrive) ||
                MustUpdateDrive(joint.angularYZDrive))
            {
                joint.angularXDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
                joint.angularYZDrive = GetDrive(DriveSpring, DriveDamper, DriveMaxForce);
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