using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class HorizontalMove : SubComponent
    {
        [Header("Debug")]
        public Vector3 TargetWalkDir = new Vector3();
        public float Speed;
        public Vector3 MoveForce = new Vector3();
        public Vector3 GroundNormal = new Vector3();

        int DefaultLayerMask = 1 << 0;

        private void Start()
        {
            control.ComponentProcessor.ProcDic.Add(PlayerFunction.SET_TARGETWALKDIRECTION, SetTargetWalkDir);
            control.ComponentProcessor.SetFloatDic.Add(SetFunction.TARGETWALKSPEED, SetWalkSpeed);
            control.ComponentProcessor.ProcDic.Add(PlayerFunction.WALK_TARGETDIRECTION, WalkToTargetDir);

            control.ComponentProcessor.ProcDic.Add(PlayerFunction.CANCEL_HORIZONTAL_VELOCITY, CancelHorizontalVelocity);
        }

        void SetTargetWalkDir()
        {
            TargetWalkDir = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                TargetWalkDir += control.transform.forward;
            }

            if (Input.GetKey(KeyCode.A))
            {
                TargetWalkDir -= control.transform.right;
            }

            if (Input.GetKey(KeyCode.S))
            {
                TargetWalkDir -= control.transform.forward;
            }

            if (Input.GetKey(KeyCode.D))
            {
                TargetWalkDir += control.transform.right;
            }

            if (Vector3.SqrMagnitude(TargetWalkDir) > 0.1f)
            {
                GroundNormal = GetGroundNormal();
                TargetWalkDir = Vector3.ProjectOnPlane(TargetWalkDir, GroundNormal);

                TargetWalkDir.Normalize();

                if (TargetWalkDir.y > 0f)
                {
                    TargetWalkDir -= Vector3.up * TargetWalkDir.y;
                    TargetWalkDir.Normalize();
                }
                else if (TargetWalkDir.y < 0f)
                {
                    TargetWalkDir.Normalize();
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
            MoveForce = TargetWalkDir.normalized * Speed;
            control.rbody.AddForce(MoveForce, ForceMode.VelocityChange);
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

        void SetWalkSpeed(float f)
        {
            Speed = f;
        }
    }
}