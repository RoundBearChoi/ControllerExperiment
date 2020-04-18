using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class MouseClick : MonoBehaviour
    {
        PlayerController control;
        TargetAngle targetAngle;
        Vector3 targetDirection = new Vector3();
        
        private void Start()
        {
            control = GameObject.FindObjectOfType<PlayerController>();
            targetAngle = GameObject.FindObjectOfType<TargetAngle>();
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
                    targetAngle.Angle = AngleCalculator.GetAngle(targetDirection.x, targetDirection.z);
                }
            }
        }
    }
}