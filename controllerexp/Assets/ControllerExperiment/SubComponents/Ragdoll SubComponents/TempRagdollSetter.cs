using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class TempRagdollSetter : MonoBehaviour
    {
        public bool DoNotSync;
        public GameObject mirrorJoint;

        Rigidbody myRigidBody;
        ConfigurableJoint myJoint;

        //starting point (anchor for the joints)
        Vector3 MirrorAnchorPosition;
        Quaternion MirrorAnchorRotation;

        private void Start()
        {
            myRigidBody = this.gameObject.GetComponent<Rigidbody>();
            myJoint = this.gameObject.GetComponent<ConfigurableJoint>();

            MirrorAnchorPosition = mirrorJoint.transform.position;
            MirrorAnchorRotation = mirrorJoint.transform.rotation;
        }

        private void FixedUpdate()
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
    }
}