using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller.states.groundstates
{
    internal class PS_Running : PlayerState
    {
        public void Ready()
        {

        }

        public void Process(PlatformerController player, float delta)
        {
            //Move Player
            Vector3 moveDirection = Vector3.Zero;
            moveDirection.X = Input.GetAxis(PlatformerInput.RightAxis, PlatformerInput.LeftAxis);
            moveDirection.Z = Input.GetAxis(PlatformerInput.BackAxis, PlatformerInput.ForwardAxis);
            moveDirection = moveDirection.Rotated(Vector3.Up, player.Gimbal.Rotation.Y).Normalized();
            PlatformerData.Velocity = new Vector3(moveDirection.X * PlatformerData.GroundedMoveSpeed, PlatformerData.Velocity.Y, moveDirection.Z * PlatformerData.GroundedMoveSpeed);
            //Turn player
            Vector2 lookDirection = new Vector2(PlatformerData.Velocity.Z, PlatformerData.Velocity.X);
            player.Model.Rotation = new Vector3(player.Model.Rotation.X, Mathf.LerpAngle(player.Model.Rotation.Y, lookDirection.Angle(), (float)delta * 12), player.Model.Rotation.Z);
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
                return PlatformerState.JumpState();
            if (Input.IsActionJustPressed(PlatformerInput.DiveAxis))
                return PlatformerState.PlayerActionStateEnumerator.DIVING;
            if (!PlatformerData.Grounded)
                return PlatformerState.PlayerActionStateEnumerator.FALL;

            if (PlatformerData.Grounded && !PlatformerInput.DirectionalInput())
                return PlatformerState.PlayerActionStateEnumerator.IDLE;

            return PlatformerState.PlayerActionStateEnumerator.RUNNING;
        }

        public int Flags()
        {
            return PlayerState.ACT_FLAG_MOVING |
                   PlayerState.ACT_FLAG_ALLOW_FIRST_PERSON |
                   PlayerState.ACT_FLAG_PAUSE_EXIT;
        }
    }
}
