using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class BezierHandler : MonoBehaviour
    {
        [Header("Bezier Attributes")]
        public int TotalBeziers;
        public float time;
        public GameObject BezierPrefab;

        [Space(10)]
        public List<NonOptimizedBezier> AllBeziers = new List<NonOptimizedBezier>();

        private void Start()
        {
            AllBeziers.Clear();

            for (int i = 0; i < TotalBeziers; i++)
            {
                InstantiateBezier(Random.Range(0, 10f), Random.Range(0, 10f));
            }
        }

        private void Update()
        {
            foreach (NonOptimizedBezier b in AllBeziers)
            {
                b.MoveCubeTraditional(Random.Range(0f, 1f));
            }
        }

        void InstantiateBezier(float z, float y)
        {
            GameObject obj = Instantiate(BezierPrefab);
            obj.transform.position = Vector3.zero + (Vector3.forward * z) + (Vector3.up * y);

            NonOptimizedBezier b = obj.GetComponent<NonOptimizedBezier>();
            AllBeziers.Add(b);
        }
    }
}