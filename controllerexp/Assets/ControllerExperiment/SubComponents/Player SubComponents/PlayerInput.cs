using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.SubComponents.Player
{
    public class PlayerInput : SubComponent
    {
        [Header("Player Input Debug")]
        [SerializeField] bool Up;
        [SerializeField] bool Down;
        [SerializeField] bool Left;
        [SerializeField] bool Right;
        [SerializeField] bool Jump;

        private void Start()
        {
            processor.DelegateGetBool(PlayerBool.PRESSED_UP, Pressed_Up);
            processor.DelegateGetBool(PlayerBool.PRESSED_DOWN, Pressed_Down);
            processor.DelegateGetBool(PlayerBool.PRESSED_LEFT, Pressed_Left);
            processor.DelegateGetBool(PlayerBool.PRESSED_RIGHT, Pressed_Right);
            processor.DelegateGetBool(PlayerBool.PRESSED_JUMP, Pressed_Jump);
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