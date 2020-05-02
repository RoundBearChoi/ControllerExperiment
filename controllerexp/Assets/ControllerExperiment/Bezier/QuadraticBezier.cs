using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class QuadraticBezier : BezierBase
    {
		Vector3 a = new Vector3();
		Vector3 b = new Vector3();

		public override void GetBezier(out Vector3 pos, List<GameObject> Checkpoints, float time)
		{
			if (Checkpoints.Count > 3)
			{
				for (int i = 3; i < Checkpoints.Count; i++)
				{
					Checkpoints.RemoveAt(i);
				}
			}

			BezierEquation.GetQuadraticCurve(out pos,
				Checkpoints[0].transform.position,
				Checkpoints[1].transform.position,
				Checkpoints[2].transform.position,
				time);
		}

		private void Update()
		{
			GetBezier(out myPosition, Checkpoints, time);
			this.transform.position = myPosition;

			DrawLines();
		}

		void DrawLines()
		{
			Debug.DrawLine(Checkpoints[0].transform.position, Checkpoints[1].transform.position, Color.green);
			Debug.DrawLine(Checkpoints[1].transform.position, Checkpoints[2].transform.position, Color.green);

			a = Vector3.Lerp(Checkpoints[0].transform.position, Checkpoints[1].transform.position, time);
			b = Vector3.Lerp(Checkpoints[1].transform.position, Checkpoints[2].transform.position, time);

			Debug.DrawLine(a, b, Color.yellow);
		}
	}
}