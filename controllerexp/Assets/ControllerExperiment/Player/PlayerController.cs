using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Found on Awake")]
        public Rigidbody rbody;
        public CapsuleCollider capCollider;

        [Header("Attributes")]
        public float TargetAngle;
        public float JumpForce;

        [Header("Debug")]
        public Vector3 TargetWalkDir = new Vector3();
        public bool Grounded;
        public bool Jumped;
        public bool JumpForceAdded;

        public Dictionary<SubComponents, SubComponent> SubComponentsDic = new Dictionary<SubComponents, SubComponent>();

        private void Awake()
        {
            JumpForceAdded = false;
            rbody = this.gameObject.GetComponent<Rigidbody>();
            capCollider = this.gameObject.GetComponent<CapsuleCollider>();
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Jumped = true;
            }

            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnUpdate();
        }

        private void OnCollisionStay(Collision col)
        {
            foreach(ContactPoint p in col.contacts)
            {
                Vector3 bottom = capCollider.bounds.center - Vector3.up * capCollider.bounds.extents.y;
                Debug.DrawLine(bottom, p.point, Color.blue, 0.5f);

                Vector3 dir = bottom - p.point;

                //float angle = 90f - Vector3.Angle(Vector3.down, dir);
                
                if (dir.sqrMagnitude < capCollider.radius)
                {
                    Grounded = true;

                    if (rbody.velocity.y < 0.01f)
                    {
                        Jumped = false;
                        JumpForceAdded = false;
                    }
                }
                else
                {
                    //Debug.Log("side collision");
                }
            }
        }

        private void FixedUpdate()
        {
            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnFixedUpdate();
            SubComponentsDic[SubComponents.ROTATION].OnFixedUpdate();

            Jump();
        }

        void CancelVerticalVelocity()
        {
            if (rbody.velocity.y > 0f)
            {
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
            }
        }

        void Jump()
        {
            if (Jumped)
            {
                if (!JumpForceAdded)
                {
                    rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
                    rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
                    JumpForceAdded = true;
                }
            }
            else
            {
                CancelVerticalVelocity();
            }

            Grounded = false;
        }
    }
}