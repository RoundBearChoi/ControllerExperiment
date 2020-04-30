using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.SubComponents.Player
{
    public class GroundDetection : SubComponent
    {
        [Header("Ground Detection Debug")]
        [SerializeField] bool m_IsGrounded;

        private void Start()
        {
            processor.DelegateGetBool(PlayerBool.IS_GROUNDED, IsGrounded);
            processor.DelegateSetBool(PlayerBool.IS_GROUNDED, SetGroundStatus);
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