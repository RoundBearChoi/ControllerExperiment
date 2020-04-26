using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class PartFinder : SubComponent
    {
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
    }
}