using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class Rotator : MonoBehaviour
    {
        void Update()
        {
            this.transform.Rotate(Vector3.up, 180f * Time.deltaTime);
        }
    }
}