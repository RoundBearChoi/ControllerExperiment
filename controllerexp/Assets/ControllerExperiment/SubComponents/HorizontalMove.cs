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
        }

        public override void OnFixedUpdate()
        {
            GetTargetWalkDir();
            WalkToTargetDir();
        }

        void GetTargetWalkDir()
        {
            TargetWalkDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                TargetWalkDir += this.transform.forward * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.A))
            {
                TargetWalkDir -= this.transform.right * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.S))
            {
                TargetWalkDir -= this.transform.forward * WalkSpeed;
            }

            if (Input.GetKey(KeyCode.D))
            {
                TargetWalkDir += this.transform.right * WalkSpeed;
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
                    TargetWalkDir *= WalkSpeed * 0.85f;
                }

                Debug.DrawLine(control.rbody.position, control.rbody.position + TargetWalkDir, Color.yellow, 1f);
            }
        }

        void WalkToTargetDir()
        {
            if (TargetWalkDir.sqrMagnitude > 0.1f)
            {
                control.rbody.AddForce(TargetWalkDir, ForceMode.VelocityChange);
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