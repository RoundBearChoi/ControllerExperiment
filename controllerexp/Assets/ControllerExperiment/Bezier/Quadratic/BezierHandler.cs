using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class BezierHandler : MonoBehaviour
    {
        public int TotalBeziers;
        public float mTime;
        public float mTimeScale;
        public GameObject BezierPrefab;
        public List<OptimizedBezier> AllBeziers = new List<OptimizedBezier>();

        private void Start()
        {
            AllBeziers.Clear();
            mTime = 0f;

            for (int i = 0; i < TotalBeziers; i++)
            {
                InstantiateBezier(Random.Range(0, 10f), Random.Range(0, 10f));
            }
        }

        private void Update()
        {
            SetTime();
            
            foreach(OptimizedBezier b in AllBeziers)
            {
                b.MoveCube();
            }
        }

        private void LateUpdate()
        {
            foreach (OptimizedBezier b in AllBeziers)
            {
                b.UpdateResult();
            }
        }

        void InstantiateBezier(float z, float y)
        {
            GameObject obj = Instantiate(BezierPrefab);
            obj.transform.position = Vector3.zero + (Vector3.forward * z) + (Vector3.up * y);

            OptimizedBezier b = obj.GetComponent<OptimizedBezier>();
            b.owner = this;
            AllBeziers.Add(b);
        }

        void SetTime()
        {
            mTime += (Time.deltaTime * mTimeScale);

            if (mTime >= 1f)
            {
                mTime = 0f;
            }
        }
    }
}