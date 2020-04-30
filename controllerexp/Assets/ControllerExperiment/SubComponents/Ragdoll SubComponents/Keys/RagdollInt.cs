using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Keys.Ragdoll
{
    public class RagdollInt : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int RAGDOLL_ANIMATION_STATE => GetKey("RAGDOLL_ANIMATION_STATE");
        public static int RAGDOLL_TRANSFORM_STATE => GetKey("RAGDOLL_TRANSFORM_STATE");
    }
}