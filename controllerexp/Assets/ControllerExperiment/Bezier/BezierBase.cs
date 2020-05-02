using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    // https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php

    public abstract class BezierBase : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] protected float time;
        [SerializeField] protected bool AutoTime;
        [Range(0.1f, 10f)]
        [SerializeField] protected float timeScale;
        [SerializeField] protected List<GameObject> Checkpoints = new List<GameObject>();

        protected Vector3 myPosition;

        public abstract void GetBezier(out Vector3 pos, List<GameObject> Checkpoints, float time);

        private IEnumerator Start()
        {
            while (true)
            {
                if (AutoTime)
                {
                    time += Time.deltaTime * timeScale;

                    if (timeScale <= 0f)
                    {
                        timeScale = 0.1f;
                    }

                    if (time >= 1f)
                    {
                        time = 0f;
                    }
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}