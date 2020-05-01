using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Keys.Ragdoll
{
    public class SetRagdoll : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int SET_RAGDOLL_DUMMY => GetKey("SET_RAGDOLL_DUMMY");
        public static int COPY_DUMMY_ANIMATION => GetKey("COPY_DUMMY_ANIMATION");
        public static int STOP_ANIMATING => GetKey("STOP_ANIMATING");
        public static int START_ANIMATING => GetKey("START_ANIMATING");
        public static int INSTANT_ROTATE_ENTITY => GetKey("INSTANT_ROTATE_ENTITY");
        public static int INSTANT_MOVE_ENTITY => GetKey("INSTANT_MOVE_ENTITY");
    }
}