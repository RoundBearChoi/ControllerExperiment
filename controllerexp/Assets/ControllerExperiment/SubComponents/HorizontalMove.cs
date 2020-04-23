using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class HorizontalMove : SubComponent
    {
        [Header("Attributes")]
        public float WalkSpeed;
        
        [Header("Debug")]
        public Vector3 TargetWalkDir = new Vector3();
        public Vector3 GroundNormal = new Vector3();

        int DefaultLayerMask = 1 << 0;

        private void Start()
        {
            control.SubComponentsDic.Add(SubComponents.MOVE_HORIZONTAL, this);
            control.ProcDic.Add(CharacterProc.CANCEL_HORIZONTALVELOCITY, CancelHorizontalVelocity);
        }

        public override void OnUpdate()
        {
            GetTargetWalkDir();
        }

        public override void OnFixedUpdate()
        {
            WalkToTargetDir();
        }

        void GetTargetWalkDir()
        {
            TargetWalkDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                TargetWalkDir += control.transform.forward * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                TargetWalkDir -= control.transform.right * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                TargetWalkDir -= control.transform.forward * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                TargetWalkDir += control.transform.right * WalkSpeed;
            }

            if (Vector3.SqrMagnitude(TargetWalkDir) > 0.1f)
            {
                GroundNormal = GetGroundNormal();
                TargetWalkDir = Vector3.ProjectOnPlane(TargetWalkDir, GroundNormal);

                TargetWalkDir.Normalize();
                TargetWalkDir *= WalkSpeed;

                if (TargetWalkDir.y > 0f)
                {
                    TargetWalkDir -= Vector3.up * TargetWalkDir.y;
                    TargetWalkDir.Normalize();
                    TargetWalkDir *= WalkSpeed * 1.15f;
                }
                else if (TargetWalkDir.y < 0f)
                {
                    TargetWalkDir.Normalize();
                    TargetWalkDir *= WalkSpeed * 1f;
                }

                Debug.DrawLine(control.rbody.position, control.rbody.position + TargetWalkDir, Color.yellow, 0.25f);
            }
        }

        void CancelHorizontalVelocity()
        {
            control.rbody.AddForce(Vector3.right * -control.rbody.velocity.x, ForceMode.VelocityChange);
            control.rbody.AddForce(Vector3.forward * -control.rbody.velocity.z, ForceMode.VelocityChange);
        }

        void WalkToTargetDir()
        {
            CancelHorizontalVelocity();

            if (TargetWalkDir.sqrMagnitude > 0.1f)
            {
                //when grounded
                if (control.Grounded && !control.JumpTriggered)
                {
                    control.rbody.AddForce(TargetWalkDir, ForceMode.VelocityChange);
                }
                //when jumped
                else if (!control.Grounded && control.JumpTriggered)
                {
                    TargetWalkDir -= (Vector3.up * TargetWalkDir.y);
                    TargetWalkDir.Normalize();
                    TargetWalkDir *= (WalkSpeed * 0.6f);
                    control.rbody.AddForce(TargetWalkDir, ForceMode.VelocityChange);
                }
                //when falling
                else if (!control.Grounded && !control.JumpTriggered)
                {
                    TargetWalkDir -= (Vector3.up * TargetWalkDir.y);
                    TargetWalkDir.Normalize();
                    TargetWalkDir *= (WalkSpeed * 0.4f);
                    control.rbody.AddForce(TargetWalkDir, ForceMode.VelocityChange);
                }
            }
        }

        Vector3 GetGroundNormal()
        {
            Ray ray = new Ray(control.rbody.position, Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 2f, DefaultLayerMask))
            {
                return hit.normal;
            }

            return Vector3.zero;
        }
    }
}