using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public static class CubicBezierEquation
    {
		static Vector3 a;
		static Vector3 b;
		static Vector3 c;
		static Vector3 d;
		static Vector3 e;

		public static void GetCurve(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float time)
		{
			float tt = time * time;
			float ttt = time * tt;

			float u = 1f - time;
			float uu = u * u;
			float uuu = u * uu;

			result = uuu * p0;
			result += 3f * uu * time * p1;
			result += 3f * u * tt * p2;
			result += ttt * p3;

			DrawLines(p0, p1, p2, p3, time);
		}

		static void DrawLines(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float time)
		{
			Debug.DrawLine(p0, p1, Color.green);
			Debug.DrawLine(p1, p2, Color.green);
			Debug.DrawLine(p2, p3, Color.green);

			a = Vector3.Lerp(p0, p1, time);
			b = Vector3.Lerp(p1, p2, time);
			c = Vector3.Lerp(p2, p3, time);

			Debug.DrawLine(a, b, Color.yellow);
			Debug.DrawLine(b, c, Color.yellow);

			d = Vector3.Lerp(a, b, time);
			e = Vector3.Lerp(b, c, time);

			Debug.DrawLine(d, e, Color.red);
		}
	}
}