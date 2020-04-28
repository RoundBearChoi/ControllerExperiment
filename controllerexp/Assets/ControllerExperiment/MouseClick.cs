using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment
{
    public class MouseClick : MonoBehaviour
    {
        PlayerController control;
        Vector3 targetDirection = new Vector3();
        
        private void Start()
        {
            control = GameObject.FindObjectOfType<PlayerController>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit = new RaycastHit();

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawLine(hit.point, control.transform.position, Color.red);
                    targetDirection = hit.point - control.transform.position;
                    float targetAngle = AngleCalculator.GetAngle(targetDirection.x, targetDirection.z);
                    control.subComponentProcessor.SetFloatDic[SetPlayerFloat.TARGET_ROTATION_ANGLE](targetAngle);
                }
            }
        }
    }
}