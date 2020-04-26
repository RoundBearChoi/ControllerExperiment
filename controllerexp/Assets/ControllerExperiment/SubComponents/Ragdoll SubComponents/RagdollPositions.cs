using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class RagdollPositions : SubComponent
    {
        [Header("Attributes")]
        public RigidbodyInterpolation interpolate;
        public CollisionDetectionMode collision;
        public bool ConnectedBodiesCollision;

        [Header("Debug")]
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();
        [Space(5)]
        public GameObject TargetRootMirror;
        public Dictionary<ConfigurableJoint, GameObject> TargetConfigurables = new Dictionary<ConfigurableJoint, GameObject>();

        private void Start()
        {
            FindCharacterJoints();
            SetCharacterJointAttributes();
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
            TargetConfigurables.Clear();

            ConfigurableJoint[] myConfigurables = processor.owner.gameObject.GetComponentsInChildren<ConfigurableJoint>();
            Transform[] all = TargetRootMirror.gameObject.GetComponentsInChildren<Transform>();

            foreach(ConfigurableJoint j in myConfigurables)
            {
                TargetConfigurables.Add(j, null);
            }

            foreach(Transform t in all)
            {
                foreach(ConfigurableJoint c in myConfigurables)
                {
                    if (t.gameObject.name.Equals(c.gameObject.name))
                    {
                        TargetConfigurables[c] = t.gameObject;
                    }
                }
            }
        }
    }
}