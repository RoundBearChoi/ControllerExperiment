using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Keys.Player
{
    public class PlayerFloat : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int SET_TARGET_WALK_SPEED => GetKey("SET_TARGET_WALK_SPEED");
        public static int GET_TARGET_WALK_SPEED => GetKey("GET_TARGET_WALK_SPEED");
        public static int SET_TARGET_ROTATION_ANGLE => GetKey("SET_TARGET_ROTATION_ANGLE");
    }
}