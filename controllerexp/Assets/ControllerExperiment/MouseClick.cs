using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class MouseClick : MonoBehaviour
    {
        CharacterController control;
        TargetAngle targetAngle;
        Vector3 targetDirection = new Vector3();
        
        private void Start()
        {
            control = GameObject.FindObjectOfType<CharacterController>();
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
                    targetAngle.Angle = Get360Angle(targetDirection.x, targetDirection.z);
                }
            }
        }

        float Get360Angle(float x, float z)
        {
            float value = (float)((Mathf.Atan2(x, z) / System.Math.PI) * 180f);
            if (value < 0)
            {
                value += 360f;
            }

            return value;
        }
    }
}