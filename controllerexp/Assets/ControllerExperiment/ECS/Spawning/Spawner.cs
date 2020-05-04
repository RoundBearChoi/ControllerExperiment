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
                        GameObject obj = GameObject.Instantiate(CubePrefab,
                            (Vector3.forward * z * spacing) + (Vector3.up * y * spacing),
                            Quaternion.identity);
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
                        CreateEntity(new float3(0f, y * spacing, z * spacing));
                        entitycount++;
                    }
                }
            }
            else if (spawnType == SpawnType.CONVERSION)
            {
                world = World.DefaultGameObjectInjectionWorld;
                entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

                for (int y = 0; y < gridY; y++)
                {
                    for (int z = 0; z < gridZ; z++)
                    {
                        conversionSettings = GameObjectConversionSettings.FromWorld(world, null);
                        Entity entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(CubePrefab, conversionSettings);

                        CreateConvertedEntity(entity, new float3(0f, y * spacing, z * spacing));
                        entitycount++;
                    }
                }
            }
        }

        void CreateEntity(float3 position)
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
                Value = Quaternion.Euler(new Vector3(0f, 0f, 0f))
            });

            // adding render values
            entityManager.AddSharedComponentData(entity, new RenderMesh {
                mesh = CubeMesh,
                material = BlueMaterial
            });

            // adding render values (used by hybrid renderer)
            entityManager.AddComponentData(entity, new LocalToWorld { });
        }

        void CreateConvertedEntity(Entity entity, float3 position)
        {
            entityManager.Instantiate(entity);
            entityManager.SetName(entity, "My Converted Entity " + entitycount);

            entityManager.AddComponentData(entity, new Translation
            {
                Value = position
            });

            entityManager.AddComponentData(entity, new Rotation
            {
                Value = Quaternion.Euler(new Vector3(0f, 45f, 0f))
            });

            entityManager.AddComponentData(entity, new LocalToWorld { });
        }
    }
}