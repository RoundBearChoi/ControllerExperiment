using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public static class AngleCalculator
    {
        public static float GetAngle(float x, float z)
        {
            //float value = (float)((Mathf.Atan2(x, z) / System.Math.PI) * 180f);
            float value = Mathf.Atan2(x, z) * Mathf.Rad2Deg;

            return value;
        }
    }
}