using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class CharacterJointFinder : SubComponent
    {
        [Header("Debug")]
        public List<CharacterJoint> CharacterJoints = new List<CharacterJoint>();

        public void FindCharacterJoints()
        {
            ClearList();

            CharacterJoint[] joints = processor.owner.gameObject.GetComponentsInChildren<CharacterJoint>();

            foreach (CharacterJoint j in joints)
            {
                if (!CharacterJoints.Contains(j))
                {
                    CharacterJoints.Add(j);
                }
            }
        }

        void ClearList()
        {
            CharacterJoints.Clear();
        }

        public void RemoveCharacterJoints()
        {
            ClearList();

            CharacterJoint[] joints = processor.owner.gameObject.GetComponentsInChildren<CharacterJoint>();

            for (int i = 0; i < joints.Length; i++)
            {
                DestroyImmediate(joints[i]);
            }
        }
    }
}