using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.SubComponents.Player
{
    public class HorizontalMove : BaseSubComponent
    {
        [Header("Move Debug")]
        [SerializeField] Vector3 TargetWalkDir = new Vector3();
        [SerializeField] float Speed;
        [SerializeField] Vector3 MoveForce = new Vector3();
        [SerializeField] Vector3 GroundNormal = new Vector3();

        int DefaultLayerMask = 1 << 0;

        private void Start()
        {
            processor.DelegateSetEntity(SetPlayer.SET_WALK_DIRECTION, SetTargetWalkDir);
            processor.DelegateSetEntity(SetPlayer.WALK_TO_TARGET_DIRECTION, WalkToTargetDir);
            processor.DelegateSetEntity(SetPlayer.CANCEL_HORIZONTAL_VELOCITY, CancelHorizontalVelocity);
            processor.DelegateSetFloat(PlayerFloat.SET_TARGET_WALK_SPEED, SetWalkSpeed);
            processor.DelegateGetFloat(PlayerFloat.GET_TARGET_WALK_SPEED, GetWalkDirectionMagnitude);
        }

        void SetTargetWalkDir()
        {
            TargetWalkDir = Vector3.zero;

            bool Up = processor.GetBool(PlayerBool.PRESSED_UP);
            bool Down = processor.GetBool(PlayerBool.PRESSED_DOWN);
            bool Left = processor.GetBool(PlayerBool.PRESSED_LEFT);
            bool Right = processor.GetBool(PlayerBool.PRESSED_RIGHT);

            if (Up)
            {
                TargetWalkDir += processor.owner.transform.forward;
            }

            if (Down)
            {
                TargetWalkDir -= processor.owner.transform.forward;
            }

            if (Left)
            {
                TargetWalkDir -= processor.owner.transform.right;
            }

            if (Right)
            {
                TargetWalkDir += processor.owner.transform.right;
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

                Debug.DrawLine(processor.owner.rbody.position, processor.owner.rbody.position + TargetWalkDir, Color.yellow, 0.25f);
            }
        }

        void CancelHorizontalVelocity()
        {
            Rigidbody ownerRigidBody = processor.owner.rbody;
            ownerRigidBody.AddForce(Vector3.right * -ownerRigidBody.velocity.x, ForceMode.VelocityChange);
            ownerRigidBody.AddForce(Vector3.forward * -ownerRigidBody.velocity.z, ForceMode.VelocityChange);
        }

        void WalkToTargetDir()
        {
            CancelHorizontalVelocity();
            MoveForce = TargetWalkDir.normalized * Speed;
            processor.owner.rbody.AddForce(MoveForce, ForceMode.VelocityChange);
        }

        Vector3 GetGroundNormal()
        {
            Ray ray = new Ray(processor.owner.rbody.position, Vector3.down);
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

        float GetWalkDirectionMagnitude()
        {
            return TargetWalkDir.sqrMagnitude;
        }
    }
}