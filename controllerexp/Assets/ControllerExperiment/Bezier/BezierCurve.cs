using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Bezier
{
	public enum BezierType
	{
		QUADRATIC,
		CUBIC,
	}

    public class BezierCurve : MonoBehaviour
    {
		[Range(0f, 1f)]
		public float time;
		public float scale;

		public BezierType mBezierCurveType;
		public bool AutoTime;
        public GameObject RedCube;
        public GameObject GreenCube;
        public GameObject BlueCube;
		public GameObject YellowCube;
		
		Vector3 pos = new Vector3();

		private IEnumerator Start()
		{
			time = 0f;

			while (true)
			{
				if (AutoTime)
				{
					time += Time.deltaTime * scale;
				}

				if (time >= 1f)
				{
					time = 0f;
				}

				yield return new WaitForEndOfFrame();
			}
		}

		private void Update()
		{
			if (mBezierCurveType == BezierType.CUBIC)
			{
				BezierPathCalculation(out pos,
					RedCube.transform.position,
					GreenCube.transform.position,
					BlueCube.transform.position,
					YellowCube.transform.position,
					time);

				this.transform.position = pos;
			}
			else if (mBezierCurveType == BezierType.QUADRATIC)
			{
				BezierPathCalculation(out pos,
					RedCube.transform.position,
					GreenCube.transform.position,
					BlueCube.transform.position,
					time);

				this.transform.position = pos;
			}
		}

		// Cubic Curve https://www.gamasutra.com/blogs/VivekTank/20180806/323709/How_to_work_with_Bezier_Curve_in_Games_with_Unity.php
		void BezierPathCalculation(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
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

		// Bezier Curves in Unity: Quadratic Curve https://youtu.be/Xwj8_z9OrFw
		void BezierPathCalculation(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, float t)
		{
			float tt = t * t;

			float u = 1 - t;
			float uu = u * u;

			result = uu * p0;
			result += 2f * u * t * p1;
			result += tt * p2;
		}
	}
}