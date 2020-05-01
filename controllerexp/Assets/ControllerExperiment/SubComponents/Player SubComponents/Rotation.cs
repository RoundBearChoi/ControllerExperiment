using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.SubComponents.Player
{
    public class Rotation : BaseSubComponent
    {
        [Header("Attributes")]
        [SerializeField] AnimationCurve TorqueMultiplier;
        [SerializeField] float MaxTorque;

        [Header("Angle Debug")]
        [SerializeField] float TargetAngle;

        float Angle;
        float AngleDifference;
        float Torque;

        private void Start()
        {
            subComponentProcessor.DelegateSetEntity(SetPlayer.ROTATE_TO_TARGET_ANGLE, RotateToTargetAngle);
            subComponentProcessor.DelegateSetEntity(SetPlayer.CANCEL_HORIZONTAL_ANGULAR_VELOCITY, CancelHorizontalAngularVelocity);
            subComponentProcessor.DelegateSetFloat(PlayerFloat.SET_TARGET_ROTATION_ANGLE, SetTargetAngle);
        }

        void RotateToTargetAngle()
        {
            Angle = AngleCalculator.GetAngle(subComponentProcessor.owner.transform.forward.x, subComponentProcessor.owner.transform.forward.z);
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

            subComponentProcessor.owner.rbody.maxAngularVelocity = MaxTorque;

            Torque = AngleDifference * TorqueMultiplier.Evaluate(Mathf.Abs(AngleDifference) / 180f) * 20f;
            subComponentProcessor.owner.rbody.AddTorque(Vector3.up * Torque, ForceMode.VelocityChange);
            CancelHorizontalAngularVelocity();
        }

        void CancelHorizontalAngularVelocity()
        {
            Rigidbody ownerRigidBody = subComponentProcessor.owner.rbody;
            ownerRigidBody.AddTorque(Vector3.up * -ownerRigidBody.angularVelocity.y, ForceMode.VelocityChange);
        }

        void SetTargetAngle(float target)
        {
            TargetAngle = target;
        }
    }
}