using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class TargetAngle : MonoBehaviour
    {
        public float Angle;

        private void Update()
        {
            this.transform.rotation = Quaternion.Euler(0f, Angle, 0f);
        }
    }
}