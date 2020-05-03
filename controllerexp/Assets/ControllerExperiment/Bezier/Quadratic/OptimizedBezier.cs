using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
    public class OptimizedBezier : MonoBehaviour
    {
        [SerializeField] GameObject Cube;
		[SerializeField] List<GameObject> Checkpoints = new List<GameObject>();
		public BezierHandler owner;

		QuadraticBezierJob job;
		JobHandle jobHandle;

		public void MoveCube()
        {
			NativeArray<Vector3> result = new NativeArray<Vector3>(1, Allocator.TempJob);

			job = new QuadraticBezierJob()
			{
				p0 = Checkpoints[0].transform.position,
				p1 = Checkpoints[1].transform.position,
				p2 = Checkpoints[2].transform.position,
				time = owner.time,
				useInefficientCode = false,
				positionArray = result
			};

			jobHandle = job.Schedule();
			jobHandle.Complete();
			Cube.transform.position = result[0];
			result.Dispose();
		}

		private void Update()
		{
			MoveCube();
		}
	}
}