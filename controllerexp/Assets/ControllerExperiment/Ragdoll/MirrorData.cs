using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Ragdoll
{
    public class MirrorData : MonoBehaviour
    {
        public MirrorData(ConfigurableJoint joint, GameObject mirroringBodyPart)
        {
            m_joint = joint;
            m_mirroringBodyPart = mirroringBodyPart;
            //basePosition = joint.connectedAnchor
        }

        public ConfigurableJoint m_joint;
        public GameObject m_mirroringBodyPart;

        //starting position in relation to connected body
        public Vector3 basePosition;

        private void Awake()
        {
            
        }

        

        public Vector3 GetCurrentRelativePosition(Vector3 worldPosition)
        {
            return worldPosition - basePosition;
        }
    }
}