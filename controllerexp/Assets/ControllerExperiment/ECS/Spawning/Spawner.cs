using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ControllerExperiment
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] Mesh CubeMesh;
        [SerializeField] Material BlueMaterial;

        private void Start()
        {
            CreateEntity();
        }

        void CreateEntity()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity entity = entityManager.CreateEntity();
            entityManager.SetName(entity, "My Spawned Entity");

            // adding position values
            entityManager.AddComponentData(entity, new Translation {
                Value = new float3(0f, 0f, 0f) 
            });

            // adding rotation values
            entityManager.AddComponentData(entity, new Rotation
            {
                Value = Quaternion.Euler(new Vector3(0f, 45f, 0f))
            });

            // adding render values
            entityManager.AddSharedComponentData(entity, new RenderMesh {
                mesh = CubeMesh,
                material = BlueMaterial
            });

            // adding render values (used by hybrid renderer)
            entityManager.AddComponentData(entity, new LocalToWorld { });
        }
    }
}