using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerExperiment.Keys.Player;

namespace ControllerExperiment.States.Player
{
    public class CheckPlayerRender : BaseState
    {
        public override void ProcStateFixedUpdate()
        {
            int r = subComponentProcessor.GetInt(PlayerInt.GET_SELECTED_PLAYER_RENDER_TYPE);

            if (r == (int)PlayerRenderType.CAPSULE)
            {
                stateProcessor.TransitionTo(typeof(RenderCapsule));
            }
            else if (r == (int)PlayerRenderType.NO_CAPSULE)
            {
                stateProcessor.TransitionTo(typeof(NoPlayerControllerRender));
            }
        }
    }
}