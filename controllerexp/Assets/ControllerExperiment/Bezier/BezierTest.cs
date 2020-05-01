using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment.Bezier
{
    public class BezierTest : MonoBehaviour
    {
		[Range(0f, 1f)]
		public float time;

        public GameObject RedCube;
        public GameObject GreenCube;
        public GameObject BlueCube;
		public GameObject YellowCube;
		public GameObject TargetCube;
		public Vector3 pos = new Vector3();

		private IEnumerator Start()
		{
			time = 0f;

			while (true)
			{
				time += Time.deltaTime;

				if (time >= 1f)
				{
					time = 0f;
				}

				yield return new WaitForEndOfFrame();
			}
		}

		private void Update()
		{
			BezierPathCalculation(out pos,
				RedCube.transform.position,
				GreenCube.transform.position,
				BlueCube.transform.position,
				YellowCube.transform.position,
				time);

			TargetCube.transform.position = pos;
		}

		void BezierPathCalculation(out Vector3 result, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
		{
			float tt = t * t;
			float ttt = t * tt;
			float u = 1.0f - t;
			float uu = u * u;
			float uuu = u * uu;

			result = uuu * p0;
			result += 3.0f * uu * t * p1;
			result += 3.0f * u * tt * p2;
			result += ttt * p3;
		}
	}
}