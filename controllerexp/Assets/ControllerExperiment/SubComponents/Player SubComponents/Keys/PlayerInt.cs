using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Keys.Player
{
    public class PlayerInt : SubComponentKey
    {

        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
        public static int GET_SELECTED_PLAYER_RENDER_TYPE => GetKey("GET_SELECTED_PLAYER_RENDER_TYPE");
    }
}