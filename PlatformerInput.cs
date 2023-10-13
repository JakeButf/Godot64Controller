using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller
{
    internal static class PlatformerInput
    {
        public readonly static string RightAxis = "move_right";
        public readonly static string LeftAxis = "move_left";
        public readonly static string ForwardAxis = "move_forward";
        public readonly static string BackAxis = "move_back";
        public readonly static string JumpAxis = "jump";
        public readonly static string DiveAxis = "dive";

        public static bool DirectionalInput()
        {
            if (Input.GetAxis(RightAxis, LeftAxis) != 0 || Input.GetAxis(ForwardAxis, BackAxis) != 0)
                return true; else return false;
        }
    }
}
