using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class MovingCubeController : MonoBehaviour
    {
        Rigidbody rbody;
        public float MoveSpeed;
        public Vector3 MoveDir;

        private void Start()
        {
            rbody = this.GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            rbody.AddForce(Vector3.forward * -rbody.velocity.z, ForceMode.VelocityChange);
            rbody.AddForce(Vector3.right * -rbody.velocity.x, ForceMode.VelocityChange);

            if (rbody.velocity.y > 0f)
            {
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
            }

            rbody.AddForce(MoveDir * MoveSpeed, ForceMode.VelocityChange);
        }
    }
}
