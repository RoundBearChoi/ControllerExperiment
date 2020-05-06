using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace ControllerExperiment
{
    public class EntityMoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.WithAny<ControllableTag>().ForEach(
                (ref Translation translation, ref Rotation rotation, in PlayerInputData inputData) =>
                {
                    float3 forward = new float3(0f, 0f, 1f);
                    float3 dir = math.mul(rotation.Value, forward);

                    float walkSpeed = 2f;
                    dir = math.normalizesafe(dir);
                    dir *= walkSpeed * deltaTime;

                    if (inputData.W)
                    {
                        translation.Value += dir;
                    }
                    
                    if (inputData.S)
                    {
                        translation.Value -= dir;
                    }
                }).Schedule();
        }
    }
}