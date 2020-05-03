using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class QuadraticBezier : BezierBase
    {
		[Header("Draw every line")]
		[SerializeField] bool UseInefficientCode;
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
			GetBezier(out myPosition, Checkpoints, time);
			Cube.transform.position = myPosition;
		}
	}
}