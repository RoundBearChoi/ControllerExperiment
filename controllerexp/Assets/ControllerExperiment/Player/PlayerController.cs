using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Found on Awake")]
        public Rigidbody rbody;

        [Header("Attributes")]
        public float TargetAngle;

        public Dictionary<SubComponents, SubComponent> SubComponentsDic = new Dictionary<SubComponents, SubComponent>();

        private void Awake()
        {
            rbody = this.gameObject.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnFixedUpdate();
            SubComponentsDic[SubComponents.ROTATION].OnFixedUpdate();

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
    }
}