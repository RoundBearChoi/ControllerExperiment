using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class CharacterController : MonoBehaviour
    {
        public float Angle;
        Rigidbody rbody;
        TargetAngle targetAngle;
        GameObject ShowTorque;

        private void Start()
        {
            rbody = this.gameObject.GetComponent<Rigidbody>();
            targetAngle = GameObject.FindObjectOfType<TargetAngle>();
            ShowTorque = GameObject.Find("Torque");

            //rbody.AddTorque(new Vector3(0f, 10f, 0f));
        }

        private void FixedUpdate()
        {
            

            Angle = AngleCalculator.GetAngle(this.transform.forward.x, this.transform.forward.z);

            float dif = (targetAngle.Angle - Angle);
            Vector3 torque = (Vector3.up * dif);
            rbody.AddTorque(Vector3.up * -rbody.angularVelocity.y, ForceMode.VelocityChange);
            rbody.AddTorque(torque, ForceMode.VelocityChange);

            ShowTorque.transform.position = this.transform.position + torque;
            //Debug.Log(dif);
        }
    }
}