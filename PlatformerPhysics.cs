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
            gravityModifier = 1f; //Reset gravity changes
            velocity = copyVelocity();

            if(PlatformerData.Grounded)
            {
                calculateFriction(player.Velocity, PlatformerData.SurfaceFriction, 0f, delta);
            }
           
            player.Velocity = velocity;
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
            return new Vector3(player.Velocity.X, player.Velocity.Y, player.Velocity.Z);
        }

    }
}
