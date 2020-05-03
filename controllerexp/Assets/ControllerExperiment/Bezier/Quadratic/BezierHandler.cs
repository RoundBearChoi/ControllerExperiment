using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class BezierHandler : MonoBehaviour
    {
        public float time;
        public float timeScale;
        public GameObject BezierPrefab;

        private IEnumerator Start()
        {
            time = 0f;

            InstantiateBezier();

            while(true)
            {
                time += (Time.deltaTime * timeScale);

                if (time >= 1f)
                {
                    time = 0f;
                }

                yield return new WaitForEndOfFrame();
            }
        }

        void InstantiateBezier()
        {
            GameObject obj = Instantiate(BezierPrefab);
            obj.transform.position = Vector3.zero;

            obj.GetComponent<OptimizedBezier>().owner = this;
        }
    }
}