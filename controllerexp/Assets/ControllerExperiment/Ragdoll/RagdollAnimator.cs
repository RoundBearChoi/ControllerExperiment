using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class RagdollAnimator : MonoBehaviour
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

        GameObject dummyJoint; 
        ConfigurableJoint m_myJoint;
        Rigidbody m_myRigidBody;

        //starting point (anchor for the joints)
        Vector3 MirrorAnchorPosition = new Vector3();
        Quaternion MirrorAnchorRotation = new Quaternion();

        public void CopyDummyAnimation()
        {
            if (!DoNotSync)
            {
                if (dummyJoint != null)
                {
                    Vector3 MirrorTargetPosition = GetTargetPosition(dummyJoint.transform.position, MirrorAnchorPosition);
                    myJoint.targetPosition = MirrorTargetPosition;
                    Debug.DrawLine(this.transform.root.gameObject.transform.transform.position, GetMyWorldTargetPosition(), Color.yellow);

                    Quaternion MirrorTargetRotation = GetTargetRotation(dummyJoint.transform.rotation, MirrorAnchorRotation);
                    myJoint.targetRotation = MirrorTargetRotation;
                }
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
            dummyJoint = mirror;
        }

        public void SetAnchors()
        {
            MirrorAnchorPosition = dummyJoint.transform.position;
            MirrorAnchorRotation = dummyJoint.transform.rotation;
        }
    }
}