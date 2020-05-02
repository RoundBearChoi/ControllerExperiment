using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class CubicBezier : BezierBase
    {
		Vector3 a = new Vector3();
		Vector3 b = new Vector3();
		Vector3 c = new Vector3();
		Vector3 d = new Vector3();
		Vector3 e = new Vector3();

		public override void GetBezier(out Vector3 pos, List<GameObject> Checkpoints, float time)
		{
			if (Checkpoints.Count > 4)
			{
				for (int i = 4; i < Checkpoints.Count; i++)
				{
					Checkpoints.RemoveAt(i);
				}
			}

			BezierEquation.GetCubicCurve(out pos,
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

			DrawLines();
		}

		void DrawLines()
		{
			Debug.DrawLine(Checkpoints[0].transform.position, Checkpoints[1].transform.position, Color.green);
			Debug.DrawLine(Checkpoints[1].transform.position, Checkpoints[2].transform.position, Color.green);
			Debug.DrawLine(Checkpoints[2].transform.position, Checkpoints[3].transform.position, Color.green);

			a = Vector3.Lerp(Checkpoints[0].transform.position, Checkpoints[1].transform.position, time);
			b = Vector3.Lerp(Checkpoints[1].transform.position, Checkpoints[2].transform.position, time);
			c = Vector3.Lerp(Checkpoints[2].transform.position, Checkpoints[3].transform.position, time);

			Debug.DrawLine(a, b, Color.yellow);
			Debug.DrawLine(b, c, Color.yellow);

			d = Vector3.Lerp(a, b, time);
			e = Vector3.Lerp(b, c, time);

			Debug.DrawLine(d, e, Color.red);
		}
	}
}