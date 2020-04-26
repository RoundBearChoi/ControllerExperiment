using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ControllerExperiment.SubComponents
{
    [CustomEditor(typeof(PartFinder))]
    public class PartFinderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            PartFinder f = (PartFinder)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Find Character Joints"))
            {
                f.FindCharacterJoints();
            }
        }
    }
}