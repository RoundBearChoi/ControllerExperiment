using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class Rotation : SubComponent
    {
        [Header("Attributes")]
        public AnimationCurve TorqueMultiplier;
        public float MaxTorque;

        [Header("Debug")]
        public float TargetAngle;

        float Angle;
        float AngleDifference;
        float Torque;

        private void Start()
        {
            processor.ProcDic.Add(PlayerFunction.ROTATE_TARGETANGLE, RotateToTargetAngle);
            processor.ProcDic.Add(PlayerFunction.CANCEL_HORIZONTAL_ANGULAR_VELOCITY, CancelHorizontalAngularVelocity);

            processor.SetFloatDic.Add(SetFloat.TARGET_ROTATIONANGLE, SetTargetAngle);
        }

        void RotateToTargetAngle()
        {
            Angle = AngleCalculator.GetAngle(processor.owner.transform.forward.x, processor.owner.transform.forward.z);
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

            processor.owner.rbody.maxAngularVelocity = MaxTorque;

            Torque = AngleDifference * TorqueMultiplier.Evaluate(Mathf.Abs(AngleDifference) / 180f) * 20f;
            processor.owner.rbody.AddTorque(Vector3.up * Torque, ForceMode.VelocityChange);
            CancelHorizontalAngularVelocity();
        }

        void CancelHorizontalAngularVelocity()
        {
            Rigidbody ownerRigidBody = processor.owner.rbody;
            ownerRigidBody.AddTorque(Vector3.up * -ownerRigidBody.angularVelocity.y, ForceMode.VelocityChange);
        }

        void SetTargetAngle(float target)
        {
            TargetAngle = target;
        }
    }
}