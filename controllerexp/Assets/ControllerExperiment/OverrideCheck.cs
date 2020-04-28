using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public static class OverrideCheck
    {
        public static bool IsOverridden(System.Type childType, System.Type parentType, string methodName)
        {
            System.Reflection.MethodInfo info = childType.GetMethod(methodName);

            if (info.DeclaringType == parentType)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}