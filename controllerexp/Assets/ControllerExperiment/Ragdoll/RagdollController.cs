using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : MonoBehaviour
    {
        public List<GameObject> RagdollParts = new List<GameObject>();
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();

        readonly RigidbodyInterpolation DefaultInterpolation = RigidbodyInterpolation.Interpolate;
        readonly CollisionDetectionMode DefaultDetectionMode = CollisionDetectionMode.Continuous;

        private void Start()
        {
            RagdollParts.Clear();
            SetupRagdollParts();
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

        public void GetCharacterJoints()
        {
            CharacterJoints.Clear();

            CharacterJoint[] joints = this.gameObject.GetComponentsInChildren<CharacterJoint>();

            foreach (CharacterJoint j in joints)
            {
                if (!CharacterJoints.Contains(j))
                {
                    CharacterJoints.Add(j);
                }
            }
        }
    }
}