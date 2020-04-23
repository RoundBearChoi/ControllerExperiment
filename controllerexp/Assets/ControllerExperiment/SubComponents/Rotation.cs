using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class Rotation : SubComponent
    {
        [Header("Attributes")]
        public AnimationCurve TorqueMultiplier;
        public float MaxTorque;

        float Angle;
        float AngleDifference;
        float Torque;

        private void Start()
        {
            control.ProcDic.Add(PlayerFunction.ROTATE_TARGETANGLE, RotateToTargetAngle);
        }

        void RotateToTargetAngle()
        {
            Angle = AngleCalculator.GetAngle(control.transform.forward.x, control.transform.forward.z);
            AngleDifference = (control.TargetAngle - Angle);

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

            control.rbody.maxAngularVelocity = MaxTorque;

            Torque = AngleDifference * TorqueMultiplier.Evaluate(Mathf.Abs(AngleDifference) / 180f) * 20f;
            control.rbody.AddTorque(Vector3.up * Torque, ForceMode.VelocityChange);
            control.rbody.AddTorque(Vector3.up * -control.rbody.angularVelocity.y, ForceMode.VelocityChange);
        }
    }
}