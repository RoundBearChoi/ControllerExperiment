using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

namespace ControllerExperiment
{
    public class RotationSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle dependencies)
        {
            float deltaTime = Time.DeltaTime;

            JobHandle handle = Entities.ForEach((ref Rotation rotation) =>
            {
                // get angle on y-axis
                quaternion yRot = quaternion.RotateY(180f * Mathf.Deg2Rad * deltaTime);

                // matrix multiplication
                rotation.Value = math.mul(rotation.Value, yRot);
            }).Schedule(dependencies);

            return handle;
        }
    }
}