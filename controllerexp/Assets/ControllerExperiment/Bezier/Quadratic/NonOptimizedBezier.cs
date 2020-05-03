using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
    public class NonOptimizedBezier : MonoBehaviour
    {
        public GameObject Cube;
		public List<GameObject> Checkpoints = new List<GameObject>();

		Vector3 pos;

		public void MoveCubeTraditional(float time)
		{
			QuadraticBezierEquation.GetCurve(out pos,
				Checkpoints[0].transform.position,
				Checkpoints[1].transform.position,
				Checkpoints[2].transform.position,
				time,
				false);

			Cube.transform.position = pos;
		}
	}
}