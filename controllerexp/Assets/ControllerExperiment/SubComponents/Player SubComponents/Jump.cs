using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class Jump : SubComponent
    {
        public float JumpForce;

        private void Start()
        {
            processor.ProcDic.Add(PlayerProcess.ADD_JUMP_FORCE, AddJumpForce);
        }

        void AddJumpForce()
        {
            processor.owner.rbody.AddForce(Vector3.up * -processor.owner.rbody.velocity.y, ForceMode.VelocityChange);
            processor.owner.rbody.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        }
    }
}