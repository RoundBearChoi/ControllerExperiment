using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public static class JointUpdater
    {
        public static void UpdateAngularDrive(ConfigurableJoint joint, float spring, float damp)
        {
            if (joint.angularXDrive.positionSpring != spring || joint.angularYZDrive.positionSpring != spring ||
                joint.angularXDrive.positionDamper != damp || joint.angularYZDrive.positionDamper != damp)
            {
                JointDrive newXDrive = GetNewDrive(spring, damp);
                newXDrive.maximumForce = joint.angularXDrive.maximumForce;
                joint.angularXDrive = newXDrive;

                JointDrive newYZDrive = GetNewDrive(spring, damp);
                newYZDrive.maximumForce = joint.angularYZDrive.maximumForce;
                joint.angularYZDrive = newYZDrive;
            }
        }

        static JointDrive GetNewDrive(float spring, float damp)
        {
            JointDrive newDrive = new JointDrive();
            newDrive.positionSpring = spring;
            newDrive.positionDamper = damp;
            return newDrive;
        }

        public static void UpdateTargetRotation(ConfigurableJoint joint, Vector3 rotation)
        {
            joint.targetRotation = Quaternion.Euler(rotation);
        }
    }
}
