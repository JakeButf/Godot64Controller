using System;
using Godot;

namespace n64proofofconcept.scripts.player.platformercontroller.states.airstates
{
	internal class PS_Wallhug: PlayerState
	{
        bool translateToFreefall = false;
        public void Ready(PlatformerController player)
        {
        }

        public void Process(PlatformerController player, float delta)
        {
            //Slow Decent When Hugging Wall
            PlatformerData.GravityMod = .25f;
            player.Physics.LookAtWithY(PlatformerData.WallNormal);
            if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
            {
                translateToFreefall = true;
            }
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (PlatformerData.Grounded)
                return PlatformerState.PlayerActionStateEnumerator.IDLE;
            if (PlatformerData.CanLedgeGrab && PlatformerData.Velocity.Y < 0)
                return PlatformerState.PlayerActionStateEnumerator.LEDGEGRAB;
            if (translateToFreefall)
                return PlatformerState.PlayerActionStateEnumerator.WALLKICK;
            if (!PlatformerData.IsWallDetected)
                return PlatformerState.PlayerActionStateEnumerator.FALL;
            return PlatformerState.PlayerActionStateEnumerator.WALLHUG;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_AIR |
                   PlayerState.ACT_FLAG_MOVING;
        }

        public override string ToString()
        {
            return "Wallhug";
        }

        
    }
}

