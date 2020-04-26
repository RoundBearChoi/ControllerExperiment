using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class CharacterJointFinder : SubComponent
    {
        [Header("Attributes")]
        public RigidbodyInterpolation interpolate;
        public CollisionDetectionMode collision;
        public bool ConnectedBodiesCollision;

        [Header("Debug")]
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();

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
    }
}