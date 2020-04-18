using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Rotation")]
        public AnimationCurve TorqueMultiplier;
        public float MaxTorque;
        float Angle;
        float AngleDifference;
        float Torque;
        TargetAngle targetAngle;
        GameObject ShowTorque;

        [Header("Horizontal Move")]
        public float WalkSpeed;
        Vector3 TargetWalkDir = new Vector3();
        
        Rigidbody rbody;

        private void Start()
        {
            rbody = this.gameObject.GetComponent<Rigidbody>();
            targetAngle = GameObject.FindObjectOfType<TargetAngle>();
            ShowTorque = GameObject.Find("Torque");
        }

        private void FixedUpdate()
        {
            RotateToTargetAngle();
            GetTargetWalkDir();
            WalkToTargetDir();
        }

        void GetTargetWalkDir()
        {
            TargetWalkDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                TargetWalkDir += this.transform.forward * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                TargetWalkDir -= this.transform.right * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                TargetWalkDir -= this.transform.forward * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                TargetWalkDir += this.transform.right * WalkSpeed;
            }
        }

        void WalkToTargetDir()
        {
            CancelHorizontalVelocity();
            
            if (TargetWalkDir.sqrMagnitude > 0.0001f)
            {
                rbody.AddForce(TargetWalkDir, ForceMode.VelocityChange);
            }
        }

        void CancelHorizontalVelocity()
        {
            rbody.AddForce(Vector3.right * -rbody.velocity.x, ForceMode.VelocityChange);
            rbody.AddForce(Vector3.forward * -rbody.velocity.z, ForceMode.VelocityChange);
        }

        void RotateToTargetAngle()
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

            rbody.maxAngularVelocity = MaxTorque;

            Torque = AngleDifference * TorqueMultiplier.Evaluate(Mathf.Abs(AngleDifference) / 180f) * 20f;
            rbody.AddTorque(Vector3.up * Torque, ForceMode.VelocityChange);
            rbody.AddTorque(Vector3.up * -rbody.angularVelocity.y, ForceMode.VelocityChange);

            ShowTorque.transform.position = this.transform.position + (Vector3.up * Torque);
        }
    }
}