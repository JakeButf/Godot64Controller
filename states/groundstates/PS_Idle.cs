using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
    internal class PS_Idle: PlayerState
    {
        public void Ready()
        {
        }

        public void Process(PlatformerController player, float delta)
        {
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
                return PlatformerState.JumpState();
            if (PlatformerInput.DirectionalInput())
                return PlatformerState.PlayerActionStateEnumerator.RUNNING;

            return PlatformerState.PlayerActionStateEnumerator.IDLE;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_STATIONARY |
                   PlayerState.ACT_FLAG_IDLE |
                   PlayerState.ACT_FLAG_ALLOW_FIRST_PERSON |
                   PlayerState.ACT_FLAG_PAUSE_EXIT;
        }
            
    }
}
