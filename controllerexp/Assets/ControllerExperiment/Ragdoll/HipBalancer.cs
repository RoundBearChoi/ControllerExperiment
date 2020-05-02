using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class HipBalancer : MonoBehaviour
    {
        [SerializeField] Vector3 HipAnchorPosition = new Vector3();
        [SerializeField] Quaternion HipAnchorRotation = new Quaternion();

        ConfigurableJoint configurableJoint;

        private void Start()
        {
            configurableJoint = this.gameObject.GetComponent<ConfigurableJoint>();
        }

        private void FixedUpdate()
        {
            //Quaternion q = Quaternion.Inverse(this.transform.rotation) * Quaternion.Euler(Vector3.up);
            //configurableJoint.targetRotation = q;
        }
    }
}