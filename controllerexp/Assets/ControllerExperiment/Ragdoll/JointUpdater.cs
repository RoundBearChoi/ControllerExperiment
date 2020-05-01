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

        public static void UpdateDrive(ConfigurableJoint joint, float spring, float damp)
        {
            if (joint.xDrive.positionSpring != spring ||
                joint.yDrive.positionSpring != spring ||
                joint.zDrive.positionSpring != spring ||
                joint.xDrive.positionDamper != damp ||
                joint.yDrive.positionDamper != damp ||
                joint.zDrive.positionDamper != damp)
            {
                JointDrive newXDrive = GetNewDrive(spring, damp);
                newXDrive.maximumForce = joint.xDrive.maximumForce;
                joint.xDrive = newXDrive;

                JointDrive newYDrive = GetNewDrive(spring, damp);
                newYDrive.maximumForce = joint.yDrive.maximumForce;
                joint.yDrive = newYDrive;

                JointDrive newZDrive = GetNewDrive(spring, damp);
                newZDrive.maximumForce = joint.zDrive.maximumForce;
                joint.zDrive = newZDrive;
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
