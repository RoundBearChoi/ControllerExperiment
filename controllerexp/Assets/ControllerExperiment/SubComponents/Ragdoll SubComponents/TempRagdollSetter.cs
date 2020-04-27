using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class TempRagdollSetter : MonoBehaviour
    {
        public bool KinematicMovement;
        
        public GameObject mirrorJoint;
        //public float PositionSyncSpeed;
        //public float RotationSyncSpeed;

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
            myRigidBody.isKinematic = KinematicMovement;

            if (KinematicMovement)
            {
                //testing..
            }
            else
            {
                Vector3 MirrorTargetPosition = mirrorJoint.transform.position - MirrorAnchorPosition;
                myJoint.targetPosition = MirrorTargetPosition;
                Debug.DrawLine(Vector3.zero, GetMyWorldTargetPosition(), Color.yellow);

                Quaternion MirrorTargetRotation = GetTargetRotation(myJoint, mirrorJoint.transform.rotation, MirrorAnchorRotation);
                myJoint.targetRotation = MirrorTargetRotation;
            }
        }

        Quaternion GetTargetRotation(ConfigurableJoint joint, Quaternion currentRotation, Quaternion originalRotation)
        {
            return Quaternion.identity * (originalRotation * Quaternion.Inverse(currentRotation));
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