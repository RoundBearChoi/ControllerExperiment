using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class QuadraticBezier : BezierBase
    {
		Vector3 a = new Vector3();
		Vector3 b = new Vector3();
		Vector3 c = new Vector3();

		public override void GetBezier(out Vector3 pos, List<GameObject> Checkpoints, float time)
		{
			if (Checkpoints.Count > 3)
			{
				for (int i = 3; i < Checkpoints.Count; i++)
				{
					Checkpoints.RemoveAt(i);
				}
			}

			QuadraticCurve(out pos,
				Checkpoints[0].transform.position,
				Checkpoints[1].transform.position,
				Checkpoints[2].transform.position,
				time);
		}

		// https://youtu.be/Xwj8_z9OrFw
		void QuadraticCurve(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			float tt = t * t;

			float u = 1 - t;
			float uu = u * u;

			result = uu * p0;
			result += 2f * u * t * p1;
			result += tt * p2;
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
			c = Vector3.Lerp(a, b, time);

			Debug.DrawLine(a, b, Color.yellow);
			Debug.DrawLine(Checkpoints[0].transform.position, c, Color.white);
		}
	}
}