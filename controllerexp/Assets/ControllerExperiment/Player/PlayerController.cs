using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.PhysicsState;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Found on Awake")]
        public Rigidbody rbody;
        public CapsuleCollider capCollider;
        public StateProcessor stateProcessor;
        public SubComponentProcessor ComponentProcessor;

        [Header("Attributes")]
        public float JumpForce;

        [Header("Debug")]
        public float TargetAngle;
        public bool IsGrounded;
        public bool JumpButtonPressed;

        private void Awake()
        {
            rbody = this.gameObject.GetComponent<Rigidbody>();
            capCollider = this.gameObject.GetComponent<CapsuleCollider>();

            //init physics state
            stateProcessor = this.gameObject.GetComponentInChildren<StateProcessor>();
            stateProcessor.TransitionTo(typeof(CheckGround));

            //subcomponents
            ComponentProcessor = this.gameObject.GetComponentInChildren<SubComponentProcessor>();
        }

        private void OnCollisionStay(Collision col)
        {
            foreach (ContactPoint p in col.contacts)
            {
                Vector3 bottom = capCollider.bounds.center - (Vector3.up * capCollider.bounds.extents.y);
                Vector3 curve = bottom + (Vector3.up * capCollider.radius);

                Vector3 dir = curve - p.point;
                Debug.DrawLine(p.point, p.point + p.normal, Color.blue, 0.25f);
     
                if (dir.y > 0f)
                {
                    IsGrounded = true;

                    Vector3 contactDir = p.point - curve;
                    float angle = Vector3.Angle(contactDir, Vector3.up);

                    angle = 180f - Mathf.Abs(angle);

                    //Debug.Log(angle);

                    if (angle > 35f)
                    {
                        //Debug.Log("fall???");
                    }
                }
            }

            //Debug.Log("colliding grounds: " + CollidingGrounds.ToString());
        }

        private void Update()
        {
            stateProcessor.UpdateState();

            JumpButtonPressed = Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            stateProcessor.FixedUpdateState();

            IsGrounded = false;
        }

        public void CancelVerticalVelocity()
        {
            if (rbody.velocity.y > 0f)
            {
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
            }
        }

        public void AddJumpForce()
        {
            rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
            rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }
    }
}