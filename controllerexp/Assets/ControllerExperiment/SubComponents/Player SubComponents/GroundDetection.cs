using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Player
{
    public class GroundDetection : SubComponent
    {
        [Header("Ground Detection Debug")]
        public bool m_IsGrounded;

        private void Start()
        {
            processor.DelegateGetBool(GetPlayerBool.IS_GROUNDED, IsGrounded);
            processor.SetBoolDic.Add(SetPlayerBool.SET_GROUND_STATUS, SetGroundStatus);
        }

        void SetGroundStatus(bool b)
        {
            m_IsGrounded = b;
        }

        bool IsGrounded()
        {
            return m_IsGrounded;
        }
    }
}