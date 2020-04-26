using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class PlayerProcess : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int SET_WALK_DIRECTION => GetKey("SET_WALK_DIRECTION");
        public static int WALK_TO_TARGET_DIRECTION => GetKey("WALK_TO_TARGET_DIRECTION");
        public static int CANCEL_HORIZONTAL_VELOCITY => GetKey("CANCEL_HORIZONTAL_VELOCITY");
        public static int CANCEL_HORIZONTAL_ANGULAR_VELOCITY => GetKey("CANCEL_HORIZONTAL_ANGULAR_VELOCITY");
        public static int ROTATE_TO_TARGET_ANGLE => GetKey("ROTATE_TO_TARGET_ANGLE");
    }
}