using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.States.Player
{
    public class NoPlayerControllerRender : BaseState
    {
        [Header("No Render Debug")]
        [SerializeField] List<Renderer> Renderers = new List<Renderer>();

        public override void OnEnter()
        {
            if (Renderers.Count == 0)
            {
                Renderer[] arr = RendererFinder.GetRenderers(stateProcessor.owner);
                Renderers.AddRange(arr);
            }

            foreach (Renderer r in Renderers)
            {
                r.enabled = false;
            }
        }

        public override void ProcStateFixedUpdate()
        {
            int t = subComponentProcessor.GetInt(PlayerInt.GET_SELECTED_PLAYER_RENDER_TYPE);

            if (t == (int)PlayerRenderType.CAPSULE)
            {
                stateProcessor.TransitionTo(typeof(RenderCapsule));
            }
        }
    }
}
