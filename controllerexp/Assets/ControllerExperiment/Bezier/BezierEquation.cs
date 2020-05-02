using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public static class BezierEquation
    {
		public static void GetQuadraticCurve(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			float tt = t * t;

			float u = 1 - t;
			float uu = u * u;

			result = uu * p0;
			result += 2f * u * t * p1;
			result += tt * p2;
		}

		public static void GetCubicCurve(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float tt = t * t;
			float ttt = t * tt;

			float u = 1.0f - t;
			float uu = u * u;
			float uuu = u * uu;

			result = uuu * p0;
			result += 3f * uu * t * p1;
			result += 3f * u * tt * p2;
			result += ttt * p3;
		}
	}
}