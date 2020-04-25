using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ControllerExperiment.Ragdoll
{
    [CustomEditor(typeof(RagdollController))]
    public class RagdollControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RagdollController r = (RagdollController)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Setup Ragdoll Parts"))
            {
                r.SetupRagdollParts();
            }

            GUILayout.Space(5);

            if (GUILayout.Button("Setup Character Joints"))
            {
                r.SetupCharacterJoints();
            }
        }
    }
}