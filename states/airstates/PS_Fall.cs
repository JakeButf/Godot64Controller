using System;
using Godot;

namespace n64proofofconcept.scripts.player.platformercontroller.states.airstates
{
	internal class PS_Fall: PlayerState
	{
		public void Ready()
		{

		}

		public void Process(PlatformerController player, float delta)
		{
            PlatformerData.Velocity += new Vector3(player.Physics.moveDirection.X * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta, 0, player.Physics.moveDirection.Z * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta);
            float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
            if (horizontalSpeed > PlatformerData.MaxAirSpeed)
            {
                float scale = PlatformerData.MaxAirSpeed / horizontalSpeed;
                PlatformerData.Velocity.X *= scale;
                PlatformerData.Velocity.Z *= scale;
            }
        }
		public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
		{
			if (PlatformerData.Grounded)
				return PlatformerState.PlayerActionStateEnumerator.IDLE;
			if (PlatformerData.IsWallDetected)
				return PlatformerState.PlayerActionStateEnumerator.WALLHUG;
			if (Input.IsActionJustPressed(PlatformerInput.DiveAxis))
				return PlatformerState.PlayerActionStateEnumerator.DIVING;
			return PlatformerState.PlayerActionStateEnumerator.FALL;
		}

        public int Flags()
		{
			return PlayerState.ACT_FLAG_AIR |
				   PlayerState.ACT_FLAG_MOVING;
		}

        public override string ToString()
        {
			return "Fall";
        }
    }
}

