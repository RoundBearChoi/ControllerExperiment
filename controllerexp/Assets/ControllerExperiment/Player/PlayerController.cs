using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Found on Awake")]
        public Rigidbody rbody;

        [Header("Rotation")]
        public AnimationCurve TorqueMultiplier;
        public float MaxTorque;
        float Angle;
        float AngleDifference;
        float Torque;
        public float TargetAngle;

        public Dictionary<SubComponents, SubComponent> SubComponentsDic = new Dictionary<SubComponents, SubComponent>();

        private void Awake()
        {
            rbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnFixedUpdate();

            RotateToTargetAngle();

            CancelHorizontalVelocity();
            CancelVerticalVelocity();
        }

        void CancelHorizontalVelocity()
        {
            rbody.AddForce(Vector3.right * -rbody.velocity.x, ForceMode.VelocityChange);
            rbody.AddForce(Vector3.forward * -rbody.velocity.z, ForceMode.VelocityChange);
        }

        void CancelVerticalVelocity()
        {
            if (rbody.velocity.y > 0f)
            {
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
            }
        }

        void RotateToTargetAngle()
        {
            Angle = AngleCalculator.GetAngle(this.transform.forward.x, this.transform.forward.z);
            AngleDifference = (TargetAngle - Angle);

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
        }
    }
}