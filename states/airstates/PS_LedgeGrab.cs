using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.airstates
{
	
	internal class PS_LedgeGrab: PlayerState
	{
		bool drop = false;
		public void Ready(PlatformerController player)
		{
		}

		public void Process(PlatformerController player, float delta)
		{
			player.Physics.LookAtWithY(PlatformerData.WallNormal);
			//Stop falling
			PlatformerData.GravityMod = 0;
			//Lock to ledge height
			player.GlobalPosition = new Vector3(player.GlobalPosition.X, PlatformerData.LedgeCollisionPoint.Y - PlatformerData.PlayerHeight, player.GlobalPosition.Z);
			PlatformerData.Velocity = Vector3.Zero;

			Vector3 inputDirection = new Vector3(
			Input.GetActionStrength(PlatformerInput.RightAxis) - Input.GetActionStrength(PlatformerInput.LeftAxis),
			0.0f,
			Input.GetActionStrength(PlatformerInput.BackAxis) - Input.GetActionStrength(PlatformerInput.ForwardAxis)).Normalized();

			Vector3 worldInputDirection = player.Gimbal.GetChild<Camera3D>(0).GlobalTransform.Basis * inputDirection;
			Vector3 playerForward = player.Model.GlobalTransform.Basis.Y; 

			float dotProduct = playerForward.Dot(worldInputDirection);

			if (dotProduct < 0)
			{
				drop = true;
			}
		}

		public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
		{
			if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
				return PlatformerState.PlayerActionStateEnumerator.WALLKICK;
			if (drop)
				return PlatformerState.PlayerActionStateEnumerator.FALL;
			return PlatformerState.PlayerActionStateEnumerator.LEDGEGRAB;
		}

		public int Flags()
		{
			return PlayerState.ACT_FLAG_AIR |
				   PlayerState.ACT_FLAG_HANGING |
				   PlayerState.ACT_FLAG_INTANGIBLE |
				   PlayerState.ACT_FLAG_INVULNERABLE;
		}

		public override string ToString()
		{
			return "Ledgegrab";
		}
	}
}
