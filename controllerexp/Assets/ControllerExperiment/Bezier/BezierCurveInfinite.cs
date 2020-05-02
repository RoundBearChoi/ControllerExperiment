using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class BezierCurveInfinite : MonoBehaviour
    {
		[Range(0f, 1f)]
		public float time;
		public float scale;
		public List<GameObject> Checkpoints = new List<GameObject>();
		Vector3 pos = new Vector3();

		private IEnumerator Start()
		{
			time = 0f;

			if (scale <= 0f)
			{
				scale = 1f;
			}

			while (true)
			{
				time += Time.deltaTime * scale;

				if (time >= 1f)
				{
					time = 0f;
				}

				yield return new WaitForEndOfFrame();
			}
		}

		private void Update()
		{
			BezierPathCalculation(out pos, Checkpoints, time);
			this.transform.position = pos;
		}

		// https://denisrizov.com/2016/06/02/bezier-curves-unity-package-included/
		/*Vector3 GetPointOnBezierCurve(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float u = 1f - t;
			float t2 = t * t;
			float u2 = u * u;
			float u3 = u2 * u;
			float t3 = t2 * t;

			Vector3 result =
				(u3) * p0 +
				(3f * u2 * t) * p1 +
				(3f * u * t2) * p2 +
				(t3) * p3;

			return result;
		}*/

		void BezierPathCalculation(out Vector3 result, List<GameObject> objs, float t)
		{
			float u = 1f - t;
			int n = objs.Count - 1;

			Vector3 head = GetMultiplier(u, 3) * objs[0].transform.position;

			Vector3 mid = Vector3.zero;

			for (int i = 1; i < objs.Count - 1; i++)
			{
				float s = n * GetMultiplier(u, n - i) * GetMultiplier(t, i);
				mid += s * objs[i].transform.position;
			}

			Vector3 tail = GetMultiplier(t, 3) * objs[n].transform.position;

			result = head + mid + tail;
		}

		float GetMultiplier(float x, int n = 1)
		{
			if (n <= 1)
			{
				return x;
			}

			float result = Mathf.Pow(x, n - 1) * x;

			return result;
		}
	}
}