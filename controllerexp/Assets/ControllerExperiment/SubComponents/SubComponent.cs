using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public enum SubComponents
    {
        NONE,

        MOVE_HORIZONTAL,

        ROTATION,
    }

    public enum BoolData
    {
        NONE,
    }

    public enum ListData
    {
        NONE,
    }

    public enum CharacterProc
    {
        NONE,
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