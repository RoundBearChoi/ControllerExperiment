using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Player
{
    public class Jump : SubComponent
    {
        public float JumpForce;

        private void Start()
        {
            processor.DelegateSetEntity(SetPlayer.ADD_JUMP_FORCE, AddJumpForce);
            processor.DelegateSetEntity(SetPlayer.CANCEL_VERTICAL_VELOCITY, CancelVerticalVelocity);
        }

        void AddJumpForce()
        {
            processor.owner.rbody.AddForce(Vector3.up * -processor.owner.rbody.velocity.y, ForceMode.VelocityChange);
            processor.owner.rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }

        void CancelVerticalVelocity()
        {
            if (processor.owner.rbody.velocity.y > 0f)
            {
                processor.owner.rbody.AddForce(Vector3.up * -processor.owner.rbody.velocity.y, ForceMode.VelocityChange);
            }
        }
    }
}