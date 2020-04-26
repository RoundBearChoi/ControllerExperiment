using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ControllerExperiment.SubComponents
{
    [CustomEditor(typeof(CharacterJointFinder))]
    public class CharacterJointFinderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterJointFinder f = (CharacterJointFinder)target;

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
        }
    }
}