using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

namespace ControllerExperiment
{
    public class Spawner : MonoBehaviour
    {
        public enum SpawnType
        {
            TRADITIONAL,
            ECS,
        }

        [SerializeField] SpawnType spawnType;
        [SerializeField] int zCount;
        [SerializeField] int yCount;
        [SerializeField] float spacing;

        [Header("Pure ECS")]
        [SerializeField] Mesh CubeMesh;
        [SerializeField] Material BlueMaterial;

        [Header("Conversion")]
        [SerializeField] GameObject CubePrefab;

        EntityManager entityManager;
        int entitycount;

        private void Start()
        {
            entitycount = 0;
            SpawnMany(zCount, yCount, spacing);
        }

        void SpawnMany(int gridZ, int gridY, float spacing)
        {
            if (spawnType == SpawnType.TRADITIONAL)
            {
                for (int y = 0; y < gridY; y++)
                {
                    for (int z = 0; z < gridZ; z++)
                    {
                        float3 position = new float3(0f, y * spacing, z * spacing);
                        Quaternion rotation = Quaternion.Euler(0f, 5f * y, 5f * z);

                        Instantiate(CubePrefab, position, rotation);
                    }
                }
            }

            else if (spawnType == SpawnType.ECS)
            {
                entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                for (int y = 0; y < gridY; y++)
                {
                    for (int z = 0; z < gridZ; z++)
                    {
                        float3 position = new float3(0f, y * spacing, z * spacing);
                        quaternion rotation = quaternion.Euler(
                            0f * Mathf.Deg2Rad,
                            5f * y * Mathf.Deg2Rad,
                            5f * z * Mathf.Deg2Rad);

                        CreateEntity(position, rotation);
                        entitycount++;
                    }
                }
            }
        }

        void CreateEntity(float3 position, quaternion rotation)
        {
            Entity entity = entityManager.CreateEntity();

            entityManager.SetName(entity, "My Spawned Entity " + entitycount);

            // adding position values
            entityManager.AddComponentData(entity, new Translation {
                Value = position
            });

            // adding rotation values
            entityManager.AddComponentData(entity, new Rotation
            {
                Value = rotation
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