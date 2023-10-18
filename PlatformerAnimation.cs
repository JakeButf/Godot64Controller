using System;
using Godot;
namespace n64proofofconcept.scripts.player.platformercontroller
{
	internal class PlatformerAnimation
	{
		internal static void UpdateAnimation(PlatformerController player)
		{
			AnimationPlayer anim = player.Anim;
			player.StepParticles.Emitting = false;
			player.WallSlideParticles.Emitting = false;
			player.SparkleParticles.Emitting = false;

			switch(player.actionState)
			{
				case PlatformerState.PlayerActionStateEnumerator.IDLE:
                    anim.Play("idle", 0.5);
					break;
				case PlatformerState.PlayerActionStateEnumerator.RUNNING:
					anim.Play("run", 0.5, 2);
                    player.StepParticles.Emitting = true;
                    break;
				case PlatformerState.PlayerActionStateEnumerator.FALL:
					anim.Play("fall", 0.5, 2);
					break;
				case PlatformerState.PlayerActionStateEnumerator.JUMP:
					anim.Play("jumpup", 0.5);
					break;
                case PlatformerState.PlayerActionStateEnumerator.DOUBLEJUMP:
                    anim.Play("jumpup", 0.5);
                    break;
                case PlatformerState.PlayerActionStateEnumerator.TRIPLEJUMP:
                    anim.Play("rollout", 0.5);
                    break;
				case PlatformerState.PlayerActionStateEnumerator.WALLHUG:
					anim.Play("wallhug_001", 0.5);
					player.WallSlideParticles.Emitting = true;
                    break;
				case PlatformerState.PlayerActionStateEnumerator.WALLKICK:
					anim.Play("fall", 0.5); //TODO: wallkick animation
					break;
				case PlatformerState.PlayerActionStateEnumerator.DIVING:
					anim.Play("dive", 0.5);
					if (PlatformerData.Grounded)
						player.StepParticles.Emitting = true;
					break;
				case PlatformerState.PlayerActionStateEnumerator.ROLLOUT:
					anim.Play("rollout", 0.5);
					break;
                case PlatformerState.PlayerActionStateEnumerator.LEDGEGRAB:
                    anim.Play("wallhug_001", 0.5); //TODO: Ledgegrab animation
                    break;
				case PlatformerState.PlayerActionStateEnumerator.GOTITEM:
					if (PlatformerData.Grounded)
						anim.Play("win", 0.5);
					else
						anim.Play("fall", 0.5);
					break;
            }
		}
	}
}

