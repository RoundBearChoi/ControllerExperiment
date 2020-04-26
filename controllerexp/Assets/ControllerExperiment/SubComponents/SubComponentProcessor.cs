using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public enum BoolData
    {
        NONE,
    }

    public enum ListData
    {
        NONE,
    }

    public enum PlayerFunction
    {
        NONE,

        SET_TARGETWALKDIRECTION,

        WALK_TARGETDIRECTION,

        CANCEL_HORIZONTAL_VELOCITY,
        CANCEL_HORIZONTAL_ANGULAR_VELOCITY,

        ROTATE_TARGETANGLE,
    }

    public enum SetFloat
    {
        NONE,
        TARGET_WALKSPEED,
        TARGET_ROTATIONANGLE,
    }
}

namespace ControllerExperiment.SubComponents
{
    public class SubComponentProcessor : MonoBehaviour
    {
        public ControllerEntity owner;

        public Dictionary<PlayerFunction, ProcDel> ProcDic = new Dictionary<PlayerFunction, ProcDel>();
        public delegate void ProcDel();

        public Dictionary<SetFloat, SetPlayer> SetFloatDic = new Dictionary<SetFloat, SetPlayer>();
        public delegate void SetPlayer(float f);

        private void Awake()
        {
            owner = this.gameObject.GetComponentInParent<ControllerEntity>();
        }
    }
}