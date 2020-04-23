using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.PhysicsState
{
    public abstract class PhysicsState: MonoBehaviour
    {
        public abstract void ProcFixedUpdate();
    }
}