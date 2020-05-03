using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
	public class QuadraticBezier : BezierBase
    {
		[Header("Interpolate every line")]
		[SerializeField] bool UseInefficientCode;

		[Header("Jobs System")]
		[SerializeField] bool UseJobs;

		[Header("Debug")]
		[SerializeField] bool DrawDebugLines;

		QuadraticBezierJob job;
		JobHandle jobHandle;

		public override void GetBezier(out Vector3 pos, List<GameObject> Checkpoints, float time)
		{
			if (Checkpoints.Count > 3)
			{
				for (int i = 3; i < Checkpoints.Count; i++)
				{
					Checkpoints.RemoveAt(i);
				}
			}

			QuadraticBezierEquation.GetCurve(out pos,
				Checkpoints[0].transform.position,
				Checkpoints[1].transform.position,
				Checkpoints[2].transform.position,
				time,
				UseInefficientCode);
		}

		private void Update()
		{
			UpdateCube();
		}

		void UpdateCube()
		{
			// jobs approach
			if (UseJobs)
			{
				// you cannot modify values inside struct returned from another component
				// bezier curve (result position) is calculated inside the struct when job is executed
				// in order to get the result you need native arrays which is the reference
				NativeArray<Vector3> result = new NativeArray<Vector3>(1, Allocator.TempJob);

				job = new QuadraticBezierJob()
				{
					p0 = Checkpoints[0].transform.position,
					p1 = Checkpoints[1].transform.position,
					p2 = Checkpoints[2].transform.position,
					time = mTime,
					useInefficientCode = UseInefficientCode,
					resultArray = result
				};

				jobHandle = job.Schedule();
				jobHandle.Complete();
				Cube.transform.position = result[0];

				// clean up unmanaged memory & prevent memory leak
				// native containers won't be handled by garbage collector
				result.Dispose();
			}

			// traditional approach
			else
			{
				GetBezier(out myPosition, Checkpoints, mTime);
				Cube.transform.position = myPosition;
			}

			// draw lines
			if (DrawDebugLines)
			{
				QuadraticBezierEquation.DrawLines(
					Checkpoints[0].transform.position,
					Checkpoints[1].transform.position,
					Checkpoints[2].transform.position,
					mTime);
			}
		}
	}
}