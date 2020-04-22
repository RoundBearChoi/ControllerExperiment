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
            foreach(ContactPoint p in col.contacts)
            {
                Vector3 bottom = capCollider.bounds.center - (Vector3.up * capCollider.bounds.extents.y);
                Vector3 curve = bottom + (Vector3.up * capCollider.radius);

                Debug.DrawLine(curve, p.point, Color.blue, 0.5f);
                Vector3 dir = curve - p.point;
                
                if (dir.y > 0f)
                {
                    Grounded = true;

                    if (JumpUpdated)
                    {
                        JumpButtonPressed = false;
                        JumpTriggered = false;
                        JumpUpdated = false;
                        ProcDic[CharacterProc.CANCEL_HORIZONTALVELOCITY]();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            SubComponentsDic[SubComponents.MOVE_HORIZONTAL].OnFixedUpdate();
            SubComponentsDic[SubComponents.ROTATION].OnFixedUpdate();

            Jump();

            if (!JumpButtonPressed && !JumpTriggered)
            {
                CancelVerticalVelocity();
            }

            Grounded = false;
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
                Debug.Log("jump triggered");
                rbody.AddForce(Vector3.up * -rbody.velocity.y, ForceMode.VelocityChange);
                rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);

                JumpButtonPressed = false;
                JumpTriggered = true;
            }
        }
    }
}