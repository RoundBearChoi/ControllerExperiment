using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ControllerExperiment.Keys
{
    public abstract class SubComponentKey
    {
        public static int GetKey(string text)
        {
            return text.GetHashCode();
        }
    }
}