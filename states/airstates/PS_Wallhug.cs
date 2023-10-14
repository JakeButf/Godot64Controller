using System;
using Godot;

namespace n64proofofconcept.scripts.player.platformercontroller.states.airstates
{
	internal class PS_Wallhug: PlayerState
	{
        bool translateToFreefall = false;
        public void Ready()
        {
        }

        public void Process(PlatformerController player, float delta)
        {
            //Slow Decent When Hugging Wall
            PlatformerData.GravityMod = .25f;
            player.Model.Transform = LookAtWithY(player.Model.Transform, PlatformerData.WallNormal, new Vector3(0, 1, 0));
            if (Input.IsActionJustPressed(PlatformerInput.JumpAxis))
            {
                translateToFreefall = true;
                Vector3 kickVelocity = (PlatformerData.WallNormal * PlatformerData.HorizontalWallkickSpeed) + new Vector3(0, PlatformerData.VerticalWallkickSpeed, 0);
                // Apply the calculated velocity
                PlatformerData.Velocity = kickVelocity;
                player.Model.RotateY(3.14159f);
            }
        }

        public PlatformerState.PlayerActionStateEnumerator CheckStateSwitch()
        {
            if (PlatformerData.Grounded)
                return PlatformerState.PlayerActionStateEnumerator.IDLE;
            if (translateToFreefall)
                return PlatformerState.PlayerActionStateEnumerator.WALLKICK;
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

        public Transform3D LookAtWithY(Transform3D trans, Vector3 newY, Vector3 vUp)
        {
            // Y vector
            trans.Basis.X = newY.Normalized();
            trans.Basis.Z = -vUp;
            trans.Basis.X = trans.Basis.Z.Cross(trans.Basis.Y).Normalized();

            // Recompute z = y cross x
            trans.Basis.Z = trans.Basis.Y.Cross(trans.Basis.X).Normalized();
            //trans.Basis.X = trans.Basis.X * -1;

            //trans.Basis = trans.Basis.Orthonormalized();

            return trans;
        }
    }
}

