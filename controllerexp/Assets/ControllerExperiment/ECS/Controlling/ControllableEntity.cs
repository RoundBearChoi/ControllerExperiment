using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Rendering;

namespace ControllerExperiment
{
    public class ControllableEntity : MonoBehaviour
    {
        [SerializeField] Mesh mesh;
        [SerializeField] Material material;

        EntityManager entityManager;
        int count;

        private void Start()
        {
            count = 0;
            SpawnControllableEntity(float3.zero);
        }

        void SpawnControllableEntity(float3 position)
        {
            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            Entity entity = entityManager.CreateEntity();
            
            entityManager.SetName(entity, "Controllable Entity " + count);

            entityManager.AddComponentData(entity, new Translation
            {
                Value = position
            });

            entityManager.AddComponentData(entity, new Rotation
            {
                Value = quaternion.identity
            }); ;

            entityManager.AddSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material
            });

            entityManager.AddComponentData(entity, new LocalToWorld { });
        }
    }
}