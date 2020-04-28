using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class TempRagdollSetter : MonoBehaviour
    {
        public bool DoNotSync;
        public Rigidbody myRigidBody
        {
            get
            {
                if (m_myRigidBody == null)
                {
                    m_myRigidBody = this.gameObject.GetComponent<Rigidbody>();
                }
                return m_myRigidBody;
            }
        }

        public ConfigurableJoint myJoint
        {
            get
            {
                if (m_myJoint == null)
                {
                    m_myJoint = this.gameObject.GetComponent<ConfigurableJoint>();
                }
                return m_myJoint;
            }
        }

        GameObject mirrorJoint;
        

        ConfigurableJoint m_myJoint;
        Rigidbody m_myRigidBody;

        //starting point (anchor for the joints)
        Vector3 MirrorAnchorPosition;
        Quaternion MirrorAnchorRotation;

        public void CopyDummyAnimation()
        {
            if (!DoNotSync)
            {
                Vector3 MirrorTargetPosition = GetTargetPosition(mirrorJoint.transform.position, MirrorAnchorPosition);
                myJoint.targetPosition = MirrorTargetPosition;
                Debug.DrawLine(this.transform.root.gameObject.transform.transform.position, GetMyWorldTargetPosition(), Color.yellow);

                Quaternion MirrorTargetRotation = GetTargetRotation(mirrorJoint.transform.rotation, MirrorAnchorRotation);
                myJoint.targetRotation = MirrorTargetRotation;
            }
        }

        Vector3 GetTargetPosition(Vector3 currentPosition, Vector3 anchorPosition)
        {
            return anchorPosition - currentPosition;
        }

        Quaternion GetTargetRotation(Quaternion currentRotation, Quaternion anchorRotation)
        {
            return Quaternion.Inverse(currentRotation) * anchorRotation;
        }

        Vector3 GetMyWorldTargetPosition()
        {
            if (myJoint.connectedBody == null)
            {
                return Vector3.zero;
            }
            else
            {
                Vector3 myTargetPosition = myRigidBody.position + myJoint.targetPosition;

                return myTargetPosition;
            }
        }

        public void SetMirrorJoint(GameObject mirror)
        {
            mirrorJoint = mirror;
        }

        public void SetAnchors()
        {
            MirrorAnchorPosition = mirrorJoint.transform.position;
            MirrorAnchorRotation = mirrorJoint.transform.rotation;
        }
    }
}