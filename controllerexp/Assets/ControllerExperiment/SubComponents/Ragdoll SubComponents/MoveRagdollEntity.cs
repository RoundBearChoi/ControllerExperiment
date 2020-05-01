using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class MoveRagdollEntity : SubComponent
    {
        [Header("Move Attributes")]
        [SerializeField] string TargetPositionObj;
        [SerializeField] GameObject TargetObj;

        [Header("Move Debug")]
        [SerializeField] Rigidbody RootPivot;
        [SerializeField] Vector3 RootPivotAnchor;

        private void Start()
        {
            RootPivot = RagdollPartFinder.GetRootJoint(processor.owner);
            RootPivotAnchor = RootPivot.transform.localPosition;
            TargetObj = GameObject.Find(TargetPositionObj);

            processor.DelegateSetEntity(SetRagdoll.INSTANT_MOVE_ENTITY, InstantMoveEntity);
        }

        void InstantMoveEntity()
        {
            if (TargetObj != null)
            {
                processor.owner.rbody.MovePosition(TargetObj.transform.position);

                //temp
                if (RootPivot == null)
                {
                    RootPivot = RagdollPartFinder.GetRootJoint(processor.owner);
                }

                RootPivot.MovePosition(processor.owner.transform.position + RootPivotAnchor);
            }
        }
    }
}