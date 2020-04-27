using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class TempRagdollSetter : MonoBehaviour
    {
        public bool KinematicMovement;
        public bool DoNotSync;
        
        public GameObject targetJoint;
        public float PositionSyncSpeed;
        public float RotationSyncSpeed;

        Rigidbody myRigidBody;
        ConfigurableJoint myJoint;

        private void Start()
        {
            myRigidBody = this.gameObject.GetComponent<Rigidbody>();
            myJoint = this.gameObject.GetComponent<ConfigurableJoint>();
        }

        private void FixedUpdate()
        {
            myRigidBody.isKinematic = KinematicMovement;

            if (KinematicMovement)
            {
                myRigidBody.useGravity = false;

                if (!DoNotSync)
                {
                    Vector3 targetPosition = Vector3.Lerp(myJoint.transform.position, GetMyAnchorPosition(), Time.deltaTime * PositionSyncSpeed);
                    myRigidBody.MovePosition(targetPosition);

                    Quaternion targetRotation = Quaternion.Lerp(myJoint.transform.rotation, targetJoint.transform.rotation, Time.deltaTime * RotationSyncSpeed);
                    myRigidBody.MoveRotation(targetRotation);

                    Debug.DrawLine(Vector3.zero, GetMyAnchorPosition(), Color.yellow, 0.5f);
                }
            }
            else
            {
                Debug.DrawLine(Vector3.zero, GetMyTargetPosition(), Color.red, 0.5f);
            }
        }

        Vector3 GetMyAnchorPosition()
        {
            if (myJoint.connectedBody == null)
            {
                return Vector3.zero;
            }
            else
            {
                Vector3 myAnchor = myJoint.connectedBody.position + myJoint.connectedAnchor;

                return myAnchor;
            }
        }

        Vector3 GetMyTargetPosition()
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