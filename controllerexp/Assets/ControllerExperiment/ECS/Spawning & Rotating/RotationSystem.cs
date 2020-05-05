using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
    public class RotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.ForEach((ref Rotation rotation) =>
            {
                // get angle on y-axis
                quaternion yRot = quaternion.RotateY(180f * Mathf.Deg2Rad * deltaTime);

                // matrix multiplication
                rotation.Value = math.mul(rotation.Value, yRot);
            }).Schedule();
        }
    }
}