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

            if (GUILayout.Button("Get Ragdoll Parts"))
            {
                r.GetRagdollParts();
            }

            if (GUILayout.Button("Get Character Joints"))
            {
                r.GetCharacterJoints();
            }
        }
    }
}