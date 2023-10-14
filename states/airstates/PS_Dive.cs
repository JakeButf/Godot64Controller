using System;
using Godot;

namespace n64proofofconcept.scripts.player.platformercontroller.states.airstates
{
	internal class PS_Dive: PlayerState
	{
        bool rotateModel = false;
        bool forceFreeFall = false;
        public void Ready()
        {
            rotateModel = true;
            PlatformerData.DiveUsed = true;
        }

        public void Process(PlatformerController player, float delta)
        {
            if(rotateModel)
            {
                PlatformerData.Velocity = new Vector3(PlatformerData.Velocity.X, PlatformerData.VerticalDiveSpeed, PlatformerData.Velocity.Z);
                PlatformerData.Velocity += new Vector3(player.Physics.moveDirection.X, 0, player.Physics.moveDirection.Z) * PlatformerData.HorizontalDiveSpeed;
                player.InstantRotateToPlayerVelocity();
                rotateModel = false;
            } else
            {
                if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
                {
                    forceFreeFall = true;
                }
                else
                {
                    PlatformerData.Velocity += new Vector3(player.Physics.moveDirection.X * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta, 0, player.Physics.moveDirection.Z * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta);
                    float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
                    if (horizontalSpeed > PlatformerData.MaxDiveSpeed)
                    {
                        float scale = PlatformerData.MaxDiveSpeed / horizontalSpeed;
                        PlatformerData.Velocity.X *= scale;
                        PlatformerData.Velocity.Z *= scale;
                    }
                }
            }
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (PlatformerData.Grounded)
                return PlatformerState.PlayerActionStateEnumerator.IDLE;
            if (forceFreeFall)
                return PlatformerState.PlayerActionStateEnumerator.FALL;
            return PlatformerState.PlayerActionStateEnumerator.DIVING;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_AIR |
                   PlayerState.ACT_FLAG_MOVING;
        }

        public override string ToString()
        {
            return "Dive";
        }
    }
}

