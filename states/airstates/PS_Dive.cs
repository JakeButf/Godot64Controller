using System;
using Godot;

namespace n64proofofconcept.scripts.player.platformercontroller.states.airstates
{
	internal class PS_Dive: PlayerState
	{
        bool forceFreeFall = false;
        PlatformerTimer rolloutTimer;
        bool rollout = false;
        bool fastRollout = false;
        public void Ready(PlatformerController player)
        {
            if(PlatformerData.Grounded) //Give vertical boost on ground
                PlatformerData.Velocity = new Vector3(PlatformerData.Velocity.X, PlatformerData.VerticalDiveSpeed, PlatformerData.Velocity.Z);

            Vector3 newVel = PlatformerData.Velocity;
            Vector3 zeroSpeedDive = PlatformerData.Velocity;

            zeroSpeedDive += player.Model.GlobalTransform.Basis.Y * PlatformerData.HorizontalDiveSpeed * 10;

            newVel += new Vector3(player.Physics.moveDirection.X, 0, player.Physics.moveDirection.Z) * PlatformerData.HorizontalDiveSpeed;
            PlatformerData.Velocity = newVel.Normalized().Length() > zeroSpeedDive.Normalized().Length() ? newVel : zeroSpeedDive;

            float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
            if (horizontalSpeed > 0)
                player.InstantRotateToPlayerVelocity();
            PlatformerData.DiveUsed = true;
        }

        public void Process(PlatformerController player, float delta)
        {
            if (PlatformerData.Grounded && rolloutTimer == null)
            {
                rolloutTimer = new PlatformerTimer(PlatformerData.JumpModWindow); //Maybe make rollout timer smaller?
            }

            if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
            {
                if (!PlatformerData.Grounded) //If in air cancel dive state
                {
                    forceFreeFall = true;
                    return;
                }
                //Grounded dive rollout
                if(!rolloutTimer.Expired())
                {
                    rollout = true;
                } else
                {
                    rollout = true;
                }
            }

            if(!PlatformerData.Grounded)
            {
                PlatformerData.Velocity += new Vector3
                    (player.Physics.moveDirection.X * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta,
                    0,
                    player.Physics.moveDirection.Z * PlatformerData.GroundedMoveSpeed * PlatformerData.AirControlFactor * delta);

                float horizontalSpeed = new Vector3(PlatformerData.Velocity.X, 0, PlatformerData.Velocity.Z).Length();
                if (horizontalSpeed > PlatformerData.MaxDiveSpeed)
                {
                    float scale = PlatformerData.MaxDiveSpeed / horizontalSpeed;
                    PlatformerData.Velocity.X *= scale;
                    PlatformerData.Velocity.Z *= scale;
                }
                player.RotateToPlayerVelocity(delta, 30f);
            } else
            {
                //Slow dive speed on ground
                PlatformerData.Velocity = PlatformerData.Velocity.Lerp(Vector3.Zero, PlatformerData.TimeToKillSpeedFromDive * delta);
            }
            
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (forceFreeFall)
                return PlatformerState.PlayerActionStateEnumerator.FALL;
            if (rollout)
                return PlatformerState.PlayerActionStateEnumerator.ROLLOUT;
            if (fastRollout)
                return PlatformerState.PlayerActionStateEnumerator.FASTROLLOUT;
            return PlatformerState.PlayerActionStateEnumerator.DIVING;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_MOVING |
                PlayerState.ACT_FLAG_ATTACKING |
                PlayerState.ACT_FLAG_SHORT_HITBOX;
        }

        public override string ToString()
        {
            return "Dive";
        }
    }
}

