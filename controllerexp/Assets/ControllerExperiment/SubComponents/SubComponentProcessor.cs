using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.SubComponents
{
    public class SubComponentProcessor : MonoBehaviour
    {
        public Dictionary<PlayerFunction, ProcDel> ProcDic = new Dictionary<PlayerFunction, ProcDel>();
        public delegate void ProcDel();

        public Dictionary<SetFunction, SetPlayer> SetFloatDic = new Dictionary<SetFunction, SetPlayer>();
        public delegate void SetPlayer(float f);
    }
}