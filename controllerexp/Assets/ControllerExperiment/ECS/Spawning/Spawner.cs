using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;

namespace ControllerExperiment
{
    public class Spawner : MonoBehaviour
    {
        private void Start()
        {
            CreateEntity();
        }

        void CreateEntity()
        {
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // an archetype is a combination of component types
            EntityArchetype archetype = entityManager.CreateArchetype(
                // struct containing values for moving & rotating
                typeof(Translation),
                typeof(Rotation),
                // struct containing values for rendering (hybrid renderer uses them)
                typeof(RenderMesh),
                typeof(RenderBounds),
                typeof(LocalToWorld)
                );
            
            Entity entity = entityManager.CreateEntity(archetype);
            entityManager.SetName(entity, "My Spawned Entity");
        }
    }
}