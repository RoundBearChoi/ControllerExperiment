using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.SubComponents;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment
{
    public static class JumpCheck
    {
        public static bool Jump(SubComponentProcessor subComponentProcessor)
        {
            bool JumpIsPressed = subComponentProcessor.GetBool(PlayerBool.PRESSED_JUMP);

            if (JumpIsPressed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}