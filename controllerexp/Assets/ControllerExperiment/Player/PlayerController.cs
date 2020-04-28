using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.States;
using ControllerExperiment.SubComponents;

namespace ControllerExperiment
{
    public class PlayerController : ControllerEntity
    {
        [HideInInspector]
        public CapsuleCollider capCollider;

        [Header("Debug")]
        public bool IsGrounded;
        public bool JumpButtonPressed;

        private void Start()
        {
            capCollider = this.gameObject.GetComponent<CapsuleCollider>();

            //init physics state
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
                    subComponentProcessor.SetBoolDic[SetPlayerBool.SET_GROUND_STATUS](true);

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
            subComponentProcessor.UpdateSubComponents();

            JumpButtonPressed = Input.GetKey(KeyCode.Space);
        }

        private void FixedUpdate()
        {
            stateProcessor.FixedUpdateState();
            subComponentProcessor.FixedUpdateSubComponents();

            subComponentProcessor.SetBoolDic[SetPlayerBool.SET_GROUND_STATUS](false);
        }
    }
}