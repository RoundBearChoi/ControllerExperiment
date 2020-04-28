using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Ragdoll
{
    public class GetRagdollInt : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int DESIRED_RAGDOLL_STATE => GetKey("GET_DESIRED_RAGDOLL_STATE");
    }
}