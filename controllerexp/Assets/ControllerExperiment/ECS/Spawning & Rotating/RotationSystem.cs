using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

namespace ControllerExperiment
{
    public class RotationSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref Rotation rotation) =>
            {
                // get angle on y-axis
                quaternion yRot = quaternion.RotateY(180f * Mathf.Deg2Rad * Time.DeltaTime);

                // matrix multiplication
                rotation.Value = math.mul(rotation.Value, yRot);
            });
        }
    }
}