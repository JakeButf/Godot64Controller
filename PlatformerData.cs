using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller
{
    internal static class PlatformerData
    {
        /// <summary>
        /// Fields
        /// </summary>
        public static Vector3 Velocity;
        public static bool Grounded;
        public static float SurfaceFriction = 1f;
        public static float GroundedMoveSpeed = 6f;
        public static int JumpIterator = 0;
        public static float CameraLerpFactor = 3f;
    }
}
