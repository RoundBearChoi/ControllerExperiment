using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using Unity.Burst;

namespace ControllerExperiment
{
	[BurstCompile]
	struct QuadraticBezierJob : IJob
	{
		public Vector3 pos;
		public Vector3 p0;
		public Vector3 p1;
		public Vector3 p2;
		public float time;
		public bool useInefficientCode;
		public NativeArray<Vector3> resultArray;

		void IJob.Execute()
		{
			if (useInefficientCode)
			{
				Vector3 a = Vector3.Lerp(p0, p1, time);
				Vector3 b = Vector3.Lerp(p1, p2, time);
				Vector3 c = Vector3.Lerp(a, b, time);
				pos = c;

				resultArray[0] = pos;
			}
			else
			{
				float tt = time * time;

				float u = 1f - time;
				float uu = u * u;

				pos = uu * p0;
				pos += 2f * u * time * p1;
				pos += tt * p2;

				resultArray[0] = pos;
			}
		}
	}

	[BurstCompile]
	struct ParallelQuadraticBezierJob : IJobParallelFor
	{
		public Vector3 pos;
		public NativeArray<Vector3> p0;
		public NativeArray<Vector3> p1;
		public NativeArray<Vector3> p2;
		public NativeArray<float> time;
		public bool useInefficientCode;
		public NativeArray<Vector3> resultArray;

		void IJobParallelFor.Execute(int index)
		{
			float tt = time[index] * time[index];

			float u = 1f - time[index];
			float uu = u * u;

			pos = uu * p0[index];
			pos += 2f * u * time[index] * p1[index];
			pos += tt * p2[index];

			resultArray[index] = pos;
		}
	}
}