using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.States.Player;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment
{
    public class PlayerController : ControllerEntity
    {
        [HideInInspector]
        public CapsuleCollider capCollider;

        private void Start()
        {
            capCollider = this.gameObject.GetComponent<CapsuleCollider>();

            //init physics state
            GetStateProcessor(STATE.PLAYER_PHYSICS).TransitionTo(typeof(CheckGround));
        }

        private void Update()
        {
            UpdateStateProcessors();
            subComponentProcessor.UpdateSubComponents();
        }

        private void FixedUpdate()
        {
            FixedUpdateStateProcessors();
            subComponentProcessor.FixedUpdateSubComponents();

            subComponentProcessor.SetBool(PlayerBool.IS_GROUNDED, false);
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
                    subComponentProcessor.SetBool(PlayerBool.IS_GROUNDED, true);

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
    }
}