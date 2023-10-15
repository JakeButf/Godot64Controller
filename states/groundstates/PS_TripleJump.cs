using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
	internal class PS_TripleJump : PlayerState
	{
		public void Ready(PlatformerController player)
		{
			PlatformerData.Velocity = new Vector3(PlatformerData.Velocity.X, PlatformerData.JumpForce * 2f, PlatformerData.Velocity.Z);
			PlatformerData.JumpIterator = 0;

            //Prevent snapping to forward vector if jumping with no velocity
            float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
            if (horizontalSpeed > 0)
                player.InstantRotateToPlayerVelocity();
        }

		public void Process(PlatformerController player, float delta)
		{
            PlatformerData.Velocity += new Vector3(player.Physics.moveDirection.X * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta, 0, player.Physics.moveDirection.Z * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta);
			float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
			if(horizontalSpeed > PlatformerData.MaxAirSpeed)
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
			if (PlatformerData.Velocity.Y <= 0)
				return PlatformerState.PlayerActionStateEnumerator.FALL;

			return PlatformerState.PlayerActionStateEnumerator.TRIPLEJUMP;
		}

		public int Flags()
		{
			return PlayerState.ACT_FLAG_AIR |
				   PlayerState.ACT_FLAG_CONTROL_JUMP_HEIGHT;
		}

        public override string ToString()
        {
            return "TripleJump";
        }

    }
}

