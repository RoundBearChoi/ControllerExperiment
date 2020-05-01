using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public abstract class RendererFinder
    {
        public static Renderer[] GetRenderers(ControllerEntity entity)
        {
            Renderer[] arr = entity.GetComponentsInChildren<Renderer>();
            return arr;
        }
    }
}