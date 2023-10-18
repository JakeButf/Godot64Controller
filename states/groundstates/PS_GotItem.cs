using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
	internal class PS_GotItem: PlayerState
	{
		PlatformerTimer timer;
		bool breakFree;
		public void Ready(PlatformerController player)
		{
			PackedScene scene = GD.Load<PackedScene>(PlatformerData.PrismPath);
			PlatformerData.PrismCSObject = scene.Instantiate() as Node3D;
			player.GetParent().GetParent().AddChild(PlatformerData.PrismCSObject);
			PlatformerData.PrismCSObject.Scale = new Vector3(.35f, .35f, .35f);
			PlatformerData.PrismGetTimer = new PlatformerTimer(3.5f);
			breakFree = false;
			PlatformerData.GravityMod = 1f;
		}

		public void Process(PlatformerController player, float delta)
		{
			PlatformerData.Velocity = new Vector3(0, PlatformerData.Velocity.Y, 0);
			PlatformerData.PrismCSObject.GlobalPosition = new Vector3(player.GlobalPosition.X, player.GlobalPosition.Y + 3f, player.GlobalPosition.Z);
			if(PlatformerData.PrismGetTimer.Expired())
			{
				breakFree = true;
				PlatformerData.PrismCSObject.QueueFree();
			}

		}

		public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
		{
			if (breakFree) //TODO: Send to hub
				return PlatformerState.PlayerActionStateEnumerator.IDLE;
			return PlatformerState.PlayerActionStateEnumerator.GOTITEM;
		}

		public int Flags()
		{
			return PlayerState.ACT_FLAG_STATIONARY |
				   PlayerState.ACT_FLAG_IDLE |
				   PlayerState.ACT_FLAG_INVULNERABLE;
		}

		public override string ToString()
		{
			return "GotItem";
		}
	}
}
