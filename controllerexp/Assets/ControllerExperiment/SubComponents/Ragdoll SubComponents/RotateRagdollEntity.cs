using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class RotateRagdollEntity : SubComponent
    {
        [Header("Attributes")]
        [SerializeField] string TargetRotationObjectName;
        [SerializeField] GameObject TargetRotationObj;
        [SerializeField] float DesiredYRotation;

        [Header("Rotation Debug")]
        [SerializeField] Rigidbody RootPivot;

        private void Start()
        {
            processor.DelegateSetEntity(SetRagdoll.ROTATE_ENTITY, RotateEntity);
            TargetRotationObj = GameObject.Find(TargetRotationObjectName);
        }

        public override void OnFixedUpdate()
        {
            if (TargetRotationObj != null)
            {
                DesiredYRotation = TargetRotationObj.transform.rotation.eulerAngles.y;
            }
        }

        void RotateEntity()
        {
            processor.owner.rbody.MoveRotation(Quaternion.Euler(0, DesiredYRotation, 0f));
            
            //temp
            if (RootPivot == null)
            {
                Rigidbody[] arr = processor.owner.gameObject.GetComponentsInChildren<Rigidbody>();

                foreach(Rigidbody r in arr)
                {
                    ConfigurableJoint j = r.GetComponent<ConfigurableJoint>();

                    if (j == null)
                    {
                        if (r.gameObject != processor.owner.gameObject)
                        {
                            RootPivot = r;
                            break;
                        }
                    }
                }
            }

            RootPivot.MoveRotation(Quaternion.Euler(0, DesiredYRotation, 0f));
        }
    }
}