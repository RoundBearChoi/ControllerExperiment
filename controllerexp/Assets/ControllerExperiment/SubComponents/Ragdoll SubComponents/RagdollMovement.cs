using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class RagdollMovement : SubComponent
    {
        [Header("Attributes")]
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
                if (TargetMirrorDic.ContainsKey(j.gameObject))
                {
                    j.targetRotation = TargetMirrorDic[j.gameObject].transform.localRotation;
                }
            }
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