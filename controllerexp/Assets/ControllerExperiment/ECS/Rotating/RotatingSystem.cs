using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

public class RotatingSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Rotation rotation) =>
        {
            //rotation.Value = quaternion.RotateY()
        });
    }
}
