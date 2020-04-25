using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Ragdoll
{
    public class Balancer : MonoBehaviour
    {
        public GameObject Hip;
        private void FixedUpdate()
        {
            this.transform.position = Hip.transform.position;
        }
    }
}