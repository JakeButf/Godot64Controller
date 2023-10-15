using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
    internal class PS_Running : PlayerState
    {
        public void Ready(PlatformerController player)
        {

        }

        public void Process(PlatformerController player, float delta)
        {
            PlatformerData.Velocity = new Vector3(player.Physics.moveDirection.X * PlatformerData.GroundedMoveSpeed, PlatformerData.Velocity.Y, player.Physics.moveDirection.Z * PlatformerData.GroundedMoveSpeed);
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
                return PlatformerState.JumpState();
            if (Input.IsActionJustPressed(PlatformerInput.DiveAxis))
                return PlatformerState.PlayerActionStateEnumerator.DIVING;
            if (!PlatformerData.Grounded)
                return PlatformerState.PlayerActionStateEnumerator.FALL;

            if (PlatformerData.Grounded && !PlatformerInput.DirectionalInput())
                return PlatformerState.PlayerActionStateEnumerator.IDLE;

            return PlatformerState.PlayerActionStateEnumerator.RUNNING;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_MOVING |
                   PlayerState.ACT_FLAG_ALLOW_FIRST_PERSON |
                   PlayerState.ACT_FLAG_PAUSE_EXIT |
                   PlayerState.ACT_FLAG_ALLOW_MODEL_ROTATION;
        }

        public override string ToString()
        {
            return "Running";
        }
    }
}
