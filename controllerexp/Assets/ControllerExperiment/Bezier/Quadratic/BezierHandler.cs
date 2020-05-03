using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
    public class BezierHandler : MonoBehaviour
    {
        [Header("Jobs")]
        public bool UseJobs;

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
            if (UseJobs)
            {
                NativeArray<Vector3> p0s = new NativeArray<Vector3>(AllBeziers.Count, Allocator.TempJob);
                NativeArray<Vector3> p1s = new NativeArray<Vector3>(AllBeziers.Count, Allocator.TempJob);
                NativeArray<Vector3> p2s = new NativeArray<Vector3>(AllBeziers.Count, Allocator.TempJob);
                NativeArray<float> randomTimes = new NativeArray<float>(AllBeziers.Capacity, Allocator.TempJob);
                NativeArray<Vector3> results = new NativeArray<Vector3>(AllBeziers.Count, Allocator.TempJob);

                for (int i = 0; i < AllBeziers.Count; i++)
                {
                    p0s[i] = AllBeziers[i].Checkpoints[0].transform.position;
                    p1s[i] = AllBeziers[i].Checkpoints[1].transform.position;
                    p2s[i] = AllBeziers[i].Checkpoints[2].transform.position;
                    randomTimes[i] = Random.Range(0f, 1f);
                }

                ParallelQuadraticBezierJob parallelJob = new ParallelQuadraticBezierJob
                {
                    p0 = p0s,
                    p1 = p1s,
                    p2 = p2s,
                    time = randomTimes,
                    useInefficientCode = false,
                    resultArray = results
                };

                JobHandle jobHandle = parallelJob.Schedule(AllBeziers.Count, 100);
                jobHandle.Complete();

                for (int i = 0; i < AllBeziers.Count; i++)
                {
                    AllBeziers[i].Cube.transform.position = results[i];
                }

                p0s.Dispose();
                p1s.Dispose();
                p2s.Dispose();
                randomTimes.Dispose();
                results.Dispose();
            }
            else
            {
                foreach (NonOptimizedBezier b in AllBeziers)
                {
                    b.MoveCubeTraditional(Random.Range(0f, 1f));
                }
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