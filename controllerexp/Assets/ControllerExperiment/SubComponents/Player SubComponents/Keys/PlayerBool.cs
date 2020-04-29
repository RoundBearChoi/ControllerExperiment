using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents.Player
{
    public class PlayerBool : SubComponentKey
    {
        public static int IS_GROUNDED => GetKey("IS_GROUNDED");

        public static int PRESSED_UP => GetKey("PRESSED_UP");
        public static int PRESSED_DOWN => GetKey("PRESSED_DOWN");
        public static int PRESSED_LEFT => GetKey("PRESSED_LEFT");
        public static int PRESSED_RIGHT => GetKey("PRESSED_RIGHT");
        public static int PRESSED_JUMP => GetKey("PRESSED_JUMP");
    }
}