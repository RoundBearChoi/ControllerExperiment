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
		[SerializeField] bool mUseInefficientCode;

		[Header("Jobs System")]
		[SerializeField] bool mUseJobs;

		[Header("Debug")]
		[SerializeField] bool mDrawDebugLines;

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
				mUseInefficientCode);
		}

		private void Update()
		{
			UpdateCube();
		}

		void UpdateCube()
		{
			// jobs approach
			if (mUseJobs)
			{
				NativeArray<Vector3> result = new NativeArray<Vector3>(1, Allocator.TempJob);

				job = new QuadraticBezierJob()
				{
					p0 = Checkpoints[0].transform.position,
					p1 = Checkpoints[1].transform.position,
					p2 = Checkpoints[2].transform.position,
					time = mTime,
					useInefficientCode = mUseInefficientCode,
					resultArray = result
				};

				jobHandle = job.Schedule();
				jobHandle.Complete();
				Cube.transform.position = result[0];
				result.Dispose();
			}

			// traditional approach
			else
			{
				GetBezier(out myPosition, Checkpoints, mTime);
				Cube.transform.position = myPosition;
			}

			// draw lines
			if (mDrawDebugLines)
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