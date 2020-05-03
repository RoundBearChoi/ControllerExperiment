using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
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
			//Debug.Log("executing bezier job..");
			QuadraticBezierEquation.GetCurve(out pos, p0, p1, p2, time, useInefficientCode);
			resultArray[0] = pos;
		}
	}

	struct ParallelQuadraticBezierJob : IJobParallelFor
	{
		public Vector3 pos;
		public NativeArray<Vector3> p0;
		public NativeArray<Vector3> p1;
		public NativeArray<Vector3> p2;
		public float time;
		public bool useInefficientCode;
		public NativeArray<Vector3> resultArray;

		void IJobParallelFor.Execute(int index)
		{
			QuadraticBezierEquation.GetCurve(out pos, p0[index], p1[index], p2[index], time, useInefficientCode);
		}
	}
}