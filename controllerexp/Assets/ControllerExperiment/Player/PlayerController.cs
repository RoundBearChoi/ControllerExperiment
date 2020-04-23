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
        public int CollidingGrounds;
        public bool JumpButtonPressed;
        public bool JumpTriggered;
        public bool JumpUpdated;

        public Dictionary<SubComponents, SubComponent> SubComponentsDic = new Dictionary<SubComponents, SubComponent>();

        public Dictionary<CharacterProc, ProcDel> ProcDic = new Dictionary<CharacterProc, ProcDel>();
        public delegate void ProcDel();

        private void Awake()
        {
            JumpTriggered = false;
            rbody = this.gameObject.GetComponent<Rigidbody>();
            capCollider = this.gameObject.GetComponent<CapsuleCollider>();

            //init physics state
            stateProcessor = this.gameObject.GetComponentInChildren<StateProcessor>();
            stateProcessor.TransitionTo(typeof(OnGround));
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                JumpButtonPressed = true;
            }

            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnUpdate();
        }

        private void OnCollisionStay(Collision col)
        {
            CollidingGrounds = 0;

            foreach (ContactPoint p in col.contacts)
            {
                Vector3 bottom = capCollider.bounds.center - (Vector3.up * capCollider.bounds.extents.y);
                Vector3 curve = bottom + (Vector3.up * capCollider.radius);

                Vector3 dir = curve - p.point;
                Debug.DrawLine(p.point, p.point + p.normal, Color.blue, 0.25f);
     
                if (dir.y > 0f)
                {
                    IsGrounded = true;
                    CollidingGrounds += 1;

                    if (JumpUpdated)
                    {
                        JumpButtonPressed = false;
                        JumpTriggered = false;
                        JumpUpdated = false;
                        ProcDic[CharacterProc.CANCEL_HORIZONTALVELOCITY]();
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

        private void FixedUpdate()
        {
            stateProcessor.FixedUpdateState();

            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnFixedUpdate();
            SubComponentsDic[SubComponents.ROTATION].OnFixedUpdate();

            Jump();

            if (!JumpButtonPressed && !JumpTriggered)
            {
                CancelVerticalVelocity();
            }

            IsGrounded = false;
            CollidingGrounds = 0;
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