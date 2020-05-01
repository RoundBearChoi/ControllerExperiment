using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class RotateRagdollEntity : SubComponent
    {
        [Header("Rotation Attributes")]
        [SerializeField] string TargetRotationObjectName;
        [SerializeField] GameObject TargetRotationObj;
        [SerializeField] float DesiredYRotation;

        [Header("Rotation Debug")]
        [SerializeField] Rigidbody RootPivot;

        private void Start()
        {
            processor.DelegateSetEntity(SetRagdoll.INSTANT_ROTATE_ENTITY, InstantRotateEntity);
            TargetRotationObj = GameObject.Find(TargetRotationObjectName);
            RootPivot = RagdollPartFinder.GetRootJoint(processor.owner);
        }

        public override void OnFixedUpdate()
        {
            if (TargetRotationObj != null)
            {
                DesiredYRotation = TargetRotationObj.transform.rotation.eulerAngles.y;
            }
        }

        void InstantRotateEntity()
        {
            processor.owner.rbody.MoveRotation(Quaternion.Euler(0, DesiredYRotation, 0f));
            
            //temp
            RootPivot.MoveRotation(Quaternion.Euler(0, DesiredYRotation, 0f));
        }
    }
}