using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ControllerExperiment
{
    public class EntityRotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            // ref for components that you write to, in for components that you only read
            Entities.WithAny<ControllableTag>().ForEach((ref Rotation rotation, in PlayerInputData inputData) =>
            {
                rotation.Value = math.mul(rotation.Value, inputData.TargetRotationDelta);
            }).Schedule();
        }
    }
}