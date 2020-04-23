using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.PhysicsState;

namespace ControllerExperiment
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Found on Awake")]
        public Rigidbody rbody;
        public CapsuleCollider capCollider;
        public StateProcessor stateProcessor;

        [Header("Attributes")]
        public float TargetAngle;
        public float JumpForce;

        [Header("Debug")]
        public Vector3 TargetWalkDir = new Vector3();
        public bool IsGrounded;
        public bool JumpButtonPressed;
        public bool JumpTriggered;
        public bool JumpUpdated;

        public Dictionary<SubComponents, SubComponent> SubComponentsDic = new Dictionary<SubComponents, SubComponent>();

        public Dictionary<PlayerFunction, ProcDel> ProcDic = new Dictionary<PlayerFunction, ProcDel>();
        public delegate void ProcDel();

        public Dictionary<SetFunction, SetPlayer> SetFloatDic = new Dictionary<SetFunction, SetPlayer>();
        public delegate void SetPlayer(float f);

        private void Awake()
        {
            JumpTriggered = false;
            rbody = this.gameObject.GetComponent<Rigidbody>();
            capCollider = this.gameObject.GetComponent<CapsuleCollider>();

            //init physics state
            stateProcessor = this.gameObject.GetComponentInChildren<StateProcessor>();
            stateProcessor.TransitionTo(typeof(CheckGround));
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

                    if (JumpUpdated)
                    {
                        JumpButtonPressed = false;
                        JumpTriggered = false;
                        JumpUpdated = false;
                        ProcDic[PlayerFunction.CANCEL_HORIZONTALVELOCITY]();
                    }

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

            if (Input.GetKey(KeyCode.Space))
            {
                JumpButtonPressed = true;
            }
        }

        private void FixedUpdate()
        {
            stateProcessor.FixedUpdateState();

            //Jump();
            //
            //if (!JumpButtonPressed && !JumpTriggered)
            //{
            //    CancelVerticalVelocity();
            //}
            //
            
            IsGrounded = false;
        }

        public void CancelVerticalVelocity()
        {
            if (rbody.velocity.y > 0f)
            {
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
            }
        }

        void Jump()
        {
            if (JumpTriggered)
            {
                JumpUpdated = true;
            }

            if (JumpButtonPressed && !JumpUpdated)
            {
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
                rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);

                JumpButtonPressed = false;
                JumpTriggered = true;
            }
        }
    }
}