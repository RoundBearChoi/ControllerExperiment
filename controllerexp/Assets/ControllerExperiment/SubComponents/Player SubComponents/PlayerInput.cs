using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class PlayerInput : SubComponent
    {
        bool Up;
        bool Down;
        bool Left;
        bool Right;
        bool Jump;

        private void Start()
        {
            processor.GetBoolDic.Add(GetPlayerBool.PRESSED_UP, Pressed_Up);
            processor.GetBoolDic.Add(GetPlayerBool.PRESSED_DOWN, Pressed_Down);
            processor.GetBoolDic.Add(GetPlayerBool.PRESSED_LEFT, Pressed_Left);
            processor.GetBoolDic.Add(GetPlayerBool.PRESSED_RIGHT, Pressed_Right);
            processor.GetBoolDic.Add(GetPlayerBool.PRESSED_JUMP, Pressed_Jump);
        }

        public override void OnUpdate()
        {
            Up = Input.GetKey(KeyCode.W);
            Down = Input.GetKey(KeyCode.S);
            Left = Input.GetKey(KeyCode.A);
            Right = Input.GetKey(KeyCode.D);
            Jump = Input.GetKey(KeyCode.Space);
        }

        bool Pressed_Up()
        {
            return Up;
        }

        bool Pressed_Down()
        {
            return Down;
        }

        bool Pressed_Left()
        {
            return Left;
        }

        bool Pressed_Right()
        {
            return Right;
        }

        bool Pressed_Jump()
        {
            return Jump;
        }
    }
}