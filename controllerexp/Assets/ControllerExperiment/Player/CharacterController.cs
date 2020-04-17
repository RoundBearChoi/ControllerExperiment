using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class CharacterController : MonoBehaviour
    {
        public AnimationCurve TorqueMultiplier;

        float Angle;
        float AngleDifference;
        Rigidbody rbody;
        TargetAngle targetAngle;
        GameObject ShowTorque;

        private void Start()
        {
            rbody = this.gameObject.GetComponent<Rigidbody>();
            targetAngle = GameObject.FindObjectOfType<TargetAngle>();
            ShowTorque = GameObject.Find("Torque");
        }

        private void FixedUpdate()
        {
            Angle = AngleCalculator.GetAngle(this.transform.forward.x, this.transform.forward.z);
            AngleDifference = (targetAngle.Angle - Angle);

            if (Mathf.Abs(AngleDifference) > 180f)
            {
                if (AngleDifference < 0f)
                {
                    AngleDifference = (360f + AngleDifference);
                }
                else if (AngleDifference > 0f)
                {
                    AngleDifference = (360f - AngleDifference) * -1f;
                }
            }

            Vector3 torque = (Vector3.up * AngleDifference);
            rbody.AddTorque(Vector3.up * -rbody.angularVelocity.y, ForceMode.VelocityChange);
            rbody.AddTorque(torque, ForceMode.VelocityChange);

            ShowTorque.transform.position = this.transform.position + torque;
        }
    }
}