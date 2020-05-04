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
        public enum SpawnType
        {
            TRADITIONAL,
            PURE_ECS,
            CONVERSION,
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

        World world;
        EntityManager entityManager;
        GameObjectConversionSettings conversionSettings;
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

            else if (spawnType == SpawnType.PURE_ECS)
            {
                entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                for (int y = 0; y < gridY; y++)
                {
                    for (int z = 0; z < gridZ; z++)
                    {
                        float3 position = new float3(0f, y * spacing, z * spacing);
                        Quaternion rotation = Quaternion.Euler(0f, 5f * y, 5f * z);

                        CreateEntity(position, rotation);
                        entitycount++;
                    }
                }
            }

            else if (spawnType == SpawnType.CONVERSION)
            {
                world = World.DefaultGameObjectInjectionWorld;
                entityManager = world.EntityManager;

                conversionSettings = GameObjectConversionSettings.FromWorld(world, null);
                Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(CubePrefab, conversionSettings);

                for (int y = 0; y < gridY; y++)
                {
                    for (int z = 0; z < gridZ; z++)
                    {
                        float3 position = new float3(0f, y * spacing, z * spacing);
                        Quaternion rotation = Quaternion.Euler(0f, 5f * y, 5f * z);

                        CreateConvertedEntity(entity, position, rotation);
                        entitycount++;
                    }
                }
            }
        }

        void CreateEntity(float3 position, Quaternion rotation)
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

        void CreateConvertedEntity(Entity entity, float3 position, Quaternion rotation)
        {
            entity = entityManager.Instantiate(entity);

            entityManager.SetName(entity, "My Converted Entity " + entitycount);

            entityManager.AddComponentData(entity, new Translation
            {
                Value = position
            });

            entityManager.AddComponentData(entity, new Rotation
            {
                Value = rotation
            });
        }
    }
}