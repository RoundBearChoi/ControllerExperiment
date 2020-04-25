﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Ragdoll
{
    public class RagdollController : MonoBehaviour
    {
        public List<GameObject> RagdollParts = new List<GameObject>();
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();
        public List<CharacterJoint> RightCharacterJoints = new List<CharacterJoint>();
        public List<CharacterJoint> LeftCharacterJoints = new List<CharacterJoint>();

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
    }
}