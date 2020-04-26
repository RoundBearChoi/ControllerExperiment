using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class SubComponentProcessor : MonoBehaviour
    {
        public ControllerEntity owner;

        public Dictionary<int, Process> ProcDic = new Dictionary<int, Process>();
        public delegate void Process();

        public Dictionary<int, SetEntity> SetFloatDic = new Dictionary<int, SetEntity>();
        public delegate void SetEntity(float f);

        private void Awake()
        {
            owner = this.gameObject.GetComponentInParent<ControllerEntity>();
        }
    }
}