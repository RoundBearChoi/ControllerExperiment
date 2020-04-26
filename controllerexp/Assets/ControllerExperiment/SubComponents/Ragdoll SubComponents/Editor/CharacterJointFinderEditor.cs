using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ControllerExperiment.SubComponents
{
    [CustomEditor(typeof(RagdollPositions))]
    public class CharacterJointFinderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RagdollPositions f = (RagdollPositions)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Find Character Joints"))
            {
                f.FindCharacterJoints();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Set Character Joint Attributes"))
            {
                f.SetCharacterJointAttributes();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Find Configurable Joint Mirror Objects"))
            {
                f.FindConfigurableJointMirrors();
            }
        }
    }
}