using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller
{
    internal class PlatformerPhysics
    {
        readonly float gravity = (float)ProjectSettings.GetSetting("physics/3d/default_gravity") * 2;
        public Vector3 moveDirection;
        float gravityModifier = 1f;
        private PlatformerController player;
        private Vector3 velocity;
        public PlatformerPhysics(PlatformerController player)
        {
            this.player = player;
            velocity = copyVelocity();
        }

        public void PhysicsProcess(float delta)
        {
            velocity = copyVelocity();

            moveDirection = Vector3.Zero;
            moveDirection.X = Input.GetAxis(PlatformerInput.RightAxis, PlatformerInput.LeftAxis);
            moveDirection.Z = Input.GetAxis(PlatformerInput.BackAxis, PlatformerInput.ForwardAxis);
            moveDirection = moveDirection.Rotated(Vector3.Up, player.Gimbal.Rotation.Y).Normalized();

            if (PlatformerData.Grounded)
            {
                calculateFriction(PlatformerData.Velocity, PlatformerData.SurfaceFriction, 0f, delta);
            }
            velocity = new Vector3(PlatformerData.Velocity.X, PlatformerData.Velocity.Y - (gravity * (float)delta * PlatformerData.GravityMod), PlatformerData.Velocity.Z);
            PlatformerData.Velocity = velocity;
        }

        public void PhysicsTickProcess(float delta)
        {
            CheckForWall();
            CheckForLedgeGrab();
        }

        void CheckForWall()
        {
            player.WallDetector.TargetPosition = player.Model.GlobalTransform.Basis.Y * PlatformerData.WallDetectionLength;

            if (player.WallDetector.IsColliding())
            {
                PlatformerData.IsWallDetected = true;
                PlatformerData.WallNormal = player.WallDetector.GetCollisionNormal();
            }
            else
                PlatformerData.IsWallDetected = false;
        }

        public void LookAtWithY(Vector3 normal)
        {
            //TODO
        }

        void CheckForLedgeGrab()
        {
            if (!PlatformerData.IsWallDetected)
            {
                PlatformerData.CanLedgeGrab = false;
                return;
            }

            //Check if player is at a ledge
            player.LedgeDetector.TargetPosition = player.Model.GlobalTransform.Basis.Y * (PlatformerData.WallDetectionLength * 1.25f);
            if(!player.LedgeDetector.IsColliding())
            {
                //Look for ledge grab
                //Check for height of the ledge
                player.LedgeHeightDetector.TargetPosition = player.Model.GlobalTransform.Basis.Z * PlatformerData.WallDetectionLength;
                if(player.LedgeHeightDetector.IsColliding() && player.LedgeHeightDetector.GetCollisionNormal() != PlatformerData.WallNormal)
                {
                    PlatformerData.CanLedgeGrab = true;
                    PlatformerData.LedgeCollisionPoint = player.LedgeHeightDetector.GetCollisionPoint();
                } else
                {
                    PlatformerData.CanLedgeGrab = false;
                }
            } else
            {
                PlatformerData.CanLedgeGrab = false;
            }

        }

        private Vector3 calculateFriction(Vector3 velocity, float friction, float stopSpeed, float delta)
        {
            float speed = velocity.Length();

            if (speed < 0.0001905f)
                return velocity;

            float control = (speed < stopSpeed) ? stopSpeed : speed;
            float drop = 0f;
            drop += control * friction * delta;

            float newSpeed = speed - drop;

            if (newSpeed < 0f)
                newSpeed = 0f;

            if (newSpeed != speed)
                newSpeed /= speed;

            return velocity * newSpeed;
        }

        private Vector3 copyVelocity()
        {
            return new Vector3(PlatformerData.Velocity.X, PlatformerData.Velocity.Y, PlatformerData.Velocity.Z);
        }

    }
}
