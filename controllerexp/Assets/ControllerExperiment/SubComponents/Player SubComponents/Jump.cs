using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.SubComponents.Player
{
    public class Jump : BaseSubComponent
    {
        [Header("Jump Attributes")]
        [SerializeField] float JumpForce;

        private void Start()
        {
            subComponentProcessor.DelegateSetEntity(SetPlayer.ADD_JUMP_FORCE, AddJumpForce);
            subComponentProcessor.DelegateSetEntity(SetPlayer.CANCEL_VERTICAL_VELOCITY, CancelVerticalVelocity);
        }

        void AddJumpForce()
        {
            subComponentProcessor.owner.rbody.AddForce(Vector3.up * -subComponentProcessor.owner.rbody.velocity.y, ForceMode.VelocityChange);
            subComponentProcessor.owner.rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }

        void CancelVerticalVelocity()
        {
            if (subComponentProcessor.owner.rbody.velocity.y > 0f)
            {
                subComponentProcessor.owner.rbody.AddForce(Vector3.up * -subComponentProcessor.owner.rbody.velocity.y, ForceMode.VelocityChange);
            }
        }
    }
}