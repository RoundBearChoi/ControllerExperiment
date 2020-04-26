using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public abstract class ControllerEntity : MonoBehaviour
    {
        [HideInInspector]
        public Rigidbody rbody;
    }
}