using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Player
{
    public class SetPlayerFloat : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int TARGET_WALKSPEED => GetKey("TARGET_WALKSPEED");
        public static int TARGET_ROTATION_ANGLE => GetKey("TARGET_ROTATION_ANGLE");
    }
}