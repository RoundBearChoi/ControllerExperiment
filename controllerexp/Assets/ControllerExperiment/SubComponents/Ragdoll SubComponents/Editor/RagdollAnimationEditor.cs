using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    [CustomEditor(typeof(RagdollAnimation))]
    public class RagdollAnimationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            RagdollAnimation f = (RagdollAnimation)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Find Ragdoll Movers"))
            {
                f.FindRagdollSetters();
            }
        }
    }
}