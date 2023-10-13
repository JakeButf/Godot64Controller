using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
	internal class PS_Jump : PlayerState
	{
		public void Ready()
		{
			PlatformerData.Velocity = new Vector3(PlatformerData.Velocity.X, PlatformerData.JumpForce, PlatformerData.Velocity.Z);

        }

		public void Process(PlatformerController player, float delta)
		{
			
		}

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
		{
			if (PlatformerData.Grounded && PlatformerData.Velocity.Y == 0)
				return PlatformerState.PlayerActionStateEnumerator.IDLE;
			if (Input.IsActionJustPressed(PlatformerInput.DiveAxis))
				return PlatformerState.PlayerActionStateEnumerator.DIVING;

			return PlatformerState.PlayerActionStateEnumerator.JUMP;
		}

		public int Flags()
		{
			return PlayerState.ACT_FLAG_AIR |
				   PlayerState.ACT_FLAG_CONTROL_JUMP_HEIGHT;
		}

    }
}

