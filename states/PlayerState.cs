using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static n64proofofconcept.scripts.player.platformercontroller.PlatformerState;

namespace n64proofofconcept.scripts.player.platformercontroller.states
{
    internal interface PlayerState
    {
        public const int ACT_FLAG_STATIONARY                 = 0x00000200;
        public const int ACT_FLAG_MOVING                     = 0x00000400;
        public const int ACT_FLAG_AIR                        = 0x00000800;
        public const int ACT_FLAG_INTANGIBLE                 = 0x00001000;
        public const int ACT_FLAG_SWIMMING                   = 0x00002000;
        public const int ACT_FLAG_SHORT_HITBOX               = 0x00004000;
        public const int ACT_FLAG_INVULNERABLE               = 0x00008000;
        public const int ACT_FLAG_ROTATE_NORMAL              = 0x00010000;
        public const int ACT_FLAG_DIVING                     = 0x00020000;
        public const int ACT_FLAG_HANGING                    = 0x00040000;
        public const int ACT_FLAG_IDLE                       = 0x00080000;
        public const int ACT_FLAG_ATTACKING                  = 0x00100000;
        public const int ACT_FLAG_CONTROL_JUMP_HEIGHT        = 0x00200000;
        public const int ACT_FLAG_ALLOW_FIRST_PERSON         = 0x00400000;
        public const int ACT_FLAG_PAUSE_EXIT                 = 0x00800000;
        public const int ACT_FLAG_ALLOW_MODEL_ROTATION       = 0x01000000;



        public void Ready(PlatformerController player);
        public void Process(PlatformerController player, float delta);
        public PlayerActionStateEnumerator CheckStateSwitch();

        public int Flags();
        public string ToString();
    }
}
