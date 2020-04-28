using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class SetRagdoll : SubComponentKey
    {
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int SET_RAGDOLL_DUMMY => GetKey("SET_RAGDOLL_DUMMY");
        public static int COPY_DUMMY_ANIMATION => GetKey("COPY_DUMMY_ANIMATION");
    }
}