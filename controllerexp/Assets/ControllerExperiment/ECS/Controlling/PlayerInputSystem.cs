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

            bool w = Input.GetKey(KeyCode.W);
            bool a = Input.GetKey(KeyCode.A);
            bool s = Input.GetKey(KeyCode.S);
            bool d = Input.GetKey(KeyCode.D);

            Entities.WithAny<ControllableTag>().ForEach((ref PlayerInputData inputData) =>
            {
                inputData.W = w;
                inputData.A = a;
                inputData.S = s;
                inputData.D = d;

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
            }).Schedule();
        }
    }
}