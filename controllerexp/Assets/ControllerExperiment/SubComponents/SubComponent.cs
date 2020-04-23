using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public enum SubComponents
    {
        NONE,
    }

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
        CANCEL_HORIZONTALVELOCITY,
        ROTATE_TARGETANGLE,
    }

    public enum SetFunction
    {
        NONE,
        TARGETWALKSPEED,
    }

    public abstract class SubComponent : MonoBehaviour
    {
        [Header("Found on Awake")]
        public PlayerController control;

        private void Awake()
        {
            control = this.gameObject.GetComponentInParent<PlayerController>();
        }

        public virtual void OnUpdate()
        {
            throw new System.NotImplementedException();
        }
        public virtual void OnFixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}