using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ControllerExperiment.SubComponents
{
    [CustomEditor(typeof(CharacterJointFinder))]
    public class CharacterJointFinderEditor : Editor
    {
        GUIStyle buttonStyle = null;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CharacterJointFinder f = (CharacterJointFinder)target;

            GUILayout.Space(10);

            if (GUILayout.Button("Find Character Joints"))
            {
                f.FindCharacterJoints();
            }

            /*GUILayout.Space(5);

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove Character Joints", GetButtonStyle(Color.white)))
            {
                f.RemoveCharacterJoints();
            }*/
        }

        GUIStyle GetButtonStyle(Color fontColor)
        {
            if (buttonStyle == null)
            {
                buttonStyle = new GUIStyle(GUI.skin.button);
            }

            buttonStyle.normal.textColor = Color.white;

            return buttonStyle;
        }
    }
}