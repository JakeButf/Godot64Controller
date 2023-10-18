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
        public static float PlayerHeight = 2f;
        public static Vector3 Velocity;
        public static bool Grounded;
        public static float SurfaceFriction = 1f;
        public static float GroundedMoveSpeed = 9f;
        public static float TimeToReachMatchTopSpeed = 10f;
        public static float MaxAirSpeed = 9f;
        public static float MaxDiveSpeed = 14f;
        public static float AirControlFactor = 2f;
        public static float AirMoveSpeed = 3f;
        public static int JumpIterator = 0;
        public static float CameraLerpFactor = 3f;
        public static float JumpForce = 8f;
        public static float GravityMod = 1f;
        public static PlatformerTimer GroundTimer;
        public static float JumpModWindow = .25f;
        public static bool IsWallDetected = false;
        public static bool CanLedgeGrab = false;
        public static Vector3 LedgeCollisionPoint;
        public static Vector3 WallNormal;
        public static float HorizontalWallkickSpeed = 7f;
        public static float VerticalWallkickSpeed = 15f;
        public static float WallDetectionLength = 15f;
        public static float HorizontalDiveSpeed = 4.5f;
        public static float VerticalDiveSpeed = 4f;
        public static float TimeToKillSpeedFromDive = 2f;
        public static bool DiveUsed = false;
        public static float HorizontalRolloutSpeed = 6.5f;
        public static float VerticalRolloutSpeed = 8f;
        public static Vector3 SavedHorizontalRolloutSpeed;
        public static Transform3D SavedTransform;
        public static string PrismPath = "";
        public static Node3D PrismCSObject;
        public static PlatformerTimer PrismGetTimer;
    }
}
