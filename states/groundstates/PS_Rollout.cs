using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
    internal class PS_Rollout: PlayerState
    {
        public void Ready(PlatformerController player)
        {
            PlatformerData.Velocity = new Vector3(PlatformerData.Velocity.X, PlatformerData.VerticalRolloutSpeed, PlatformerData.Velocity.Z);
           // PlatformerData.Velocity += new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z);
            PlatformerData.SavedHorizontalRolloutSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z);
        }

        public void Process(PlatformerController player, float delta)
        {
            PlatformerData.Velocity = new Vector3(PlatformerData.SavedHorizontalRolloutSpeed.X, PlatformerData.Velocity.Y, PlatformerData.SavedHorizontalRolloutSpeed.Z);
            PlatformerData.Velocity += new Vector3(player.Physics.moveDirection.X * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta, 0, player.Physics.moveDirection.Z * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta);
            float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
            if (horizontalSpeed > PlatformerData.SavedHorizontalRolloutSpeed.Length())
            {
                float scale = PlatformerData.SavedHorizontalRolloutSpeed.Length() / horizontalSpeed;
                PlatformerData.Velocity.X *= scale;
                PlatformerData.Velocity.Z *= scale;
            }
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (PlatformerData.Grounded)
                return PlatformerState.PlayerActionStateEnumerator.IDLE;
            return PlatformerState.PlayerActionStateEnumerator.ROLLOUT;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_MOVING |
                PlayerState.ACT_FLAG_ATTACKING |
                PlayerState.ACT_FLAG_SHORT_HITBOX;
        }

        public override string ToString()
        {
            return "Rollout";
        }
    }
}
