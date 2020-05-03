using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
    public class NonOptimizedBezier : MonoBehaviour
    {
        [SerializeField] GameObject Cube;
		[SerializeField] List<GameObject> Checkpoints = new List<GameObject>();
		public BezierHandler owner;

		Vector3 pos;

		public void MoveCubeTraditional()
		{
			QuadraticBezierEquation.GetCurve(out pos,
				Checkpoints[0].transform.position,
				Checkpoints[1].transform.position,
				Checkpoints[2].transform.position,
				owner.time,
				false);

			Cube.transform.position = pos;
		}
	}
}