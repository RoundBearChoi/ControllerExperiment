using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
	struct QuadraticBezierJob : IJob
	{
		public Vector3 position;
		public Vector3 p0;
		public Vector3 p1;
		public Vector3 p2;
		public float time;
		public bool useInefficientCode;
		public NativeArray<Vector3> positionArray;

		void IJob.Execute()
		{
			//Debug.Log("executing bezier job..");
			QuadraticBezierEquation.GetCurve(out position, p0, p1, p2, time, useInefficientCode);
			positionArray[0] = position;
		}
	}
}