using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Ragdoll;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class MoveRagdollEntity : BaseSubComponent
    {
        [Header("Move Attributes")]
        [SerializeField] string TargetPositionObj;
        [SerializeField] GameObject TargetObj;
        [SerializeField] Vector3 Offset = new Vector3();

        [Header("Move Debug")]
        [SerializeField] Rigidbody RootPivot;
        [SerializeField] Vector3 RootPivotAnchor;

        private void Start()
        {
            RootPivot = RagdollPartFinder.GetRootJoint(subComponentProcessor.owner);
            RootPivotAnchor = RootPivot.transform.localPosition;
            TargetObj = GameObject.Find(TargetPositionObj);

            subComponentProcessor.DelegateSetEntity(SetRagdoll.INSTANT_MOVE_ENTITY, InstantMoveEntity);
        }

        void InstantMoveEntity()
        {
            if (TargetObj != null)
            {
                subComponentProcessor.owner.rbody.MovePosition(TargetObj.transform.position + Offset);

                // make sure root pivot stays anchored
                if (RootPivot == null)
                {
                    RootPivot = RagdollPartFinder.GetRootJoint(subComponentProcessor.owner);
                }

                RootPivot.MovePosition(subComponentProcessor.owner.transform.position + RootPivotAnchor);
            }
        }
    }
}