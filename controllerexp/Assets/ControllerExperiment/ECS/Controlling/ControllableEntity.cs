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
        [SerializeField] int TotalEntities;
        [SerializeField] float Spacing;

        EntityManager entityManager;
        int count;

        private void Start()
        {
            count = 0;

            for (int i = 0; i < TotalEntities; i++)
            {
                SpawnControllableEntity(new float3(0f, 0f, i * Spacing));
            }
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

            entityManager.AddComponentData(entity, new ControllableTag { });

            entityManager.AddComponentData(entity, new PlayerInputData {
                TargetRotationDelta = quaternion.identity
            });
        }
    }
}