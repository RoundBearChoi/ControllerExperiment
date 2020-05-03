using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class CubicBezier : BezierBase
    {
		public override void GetBezier(out Vector3 pos, List<GameObject> Checkpoints, float time)
		{
			if (Checkpoints.Count > 4)
			{
				for (int i = 4; i < Checkpoints.Count; i++)
				{
					Checkpoints.RemoveAt(i);
				}
			}

			CubicBezierEquation.GetCurve(out pos,
				Checkpoints[0].transform.position,
				Checkpoints[1].transform.position,
				Checkpoints[2].transform.position,
				Checkpoints[3].transform.position,
				time);
		}

		private void Update()
		{
			GetBezier(out myPosition, Checkpoints, time);
			this.transform.position = myPosition;
		}
	}
}