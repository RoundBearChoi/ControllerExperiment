using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
	// https://denisrizov.com/2016/06/02/bezier-curves-unity-package-included/

	public static class QuadraticBezierEquation
    {
		static Vector3 a;
		static Vector3 b;
		static Vector3 c;

		public static void GetCurve(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, float time, bool UseInefficientCode)
		{
			if (!UseInefficientCode)
			{
				_Efficient(out result, p0, p1, p2, time);
			}
			else
			{
				_Inefficient(out result, p0, p1, p2, time);
			}
		}

		static void _Inefficient(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, float time)
		{
			//Debug.Log("inefficient equation..");
			a = Vector3.Lerp(p0, p1, time);
			b = Vector3.Lerp(p1, p2, time);
			c = Vector3.Lerp(a, b, time);
			result = c;
		}

		public static void _Efficient(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, float time)
		{
			//Debug.Log("efficient equation..");
			float tt = time * time;

			float u = 1f - time;
			float uu = u * u;

			result = uu * p0;
			result += 2f * u * time * p1;
			result += tt * p2;
		}

		public static void DrawLines(Vector3 p0, Vector3 p1, Vector3 p2, float time)
		{
			Debug.DrawLine(p0, p1, Color.green);
			Debug.DrawLine(p1, p2, Color.green);

			a = Vector3.Lerp(p0, p1, time);
			b = Vector3.Lerp(p1, p2, time);

			Debug.DrawLine(a, b, Color.yellow);
		}
	}
}