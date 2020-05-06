using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ControllerExperiment
{
    public struct ControllableTag : IComponentData { }

    public class PlayerInputSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            float deltaTime = Time.DeltaTime;

            Entities.WithAny<ControllableTag>().ForEach((ref PlayerInputData inputData) =>
            {
                inputData.W = Input.GetKey(KeyCode.W);
                inputData.A = Input.GetKey(KeyCode.A);
                inputData.S = Input.GetKey(KeyCode.S);
                inputData.D = Input.GetKey(KeyCode.D);

                quaternion targetRotationDelta = quaternion.identity;

                if (inputData.A)
                {
                    targetRotationDelta = quaternion.RotateY(-180f * Mathf.Deg2Rad * deltaTime);
                }
                
                if (inputData.D)
                {
                    targetRotationDelta = quaternion.RotateY(180f * Mathf.Deg2Rad * deltaTime);
                }

                inputData.TargetRotationDelta = targetRotationDelta;
            }).Run();
        }
    }
}