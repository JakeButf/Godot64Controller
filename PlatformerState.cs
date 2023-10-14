using n64proofofconcept.scripts.player.platformercontroller.states;
using n64proofofconcept.scripts.player.platformercontroller.states.groundstates;
using n64proofofconcept.scripts.player.platformercontroller.states.airstates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n64proofofconcept.scripts.player.platformercontroller
{
    internal class PlatformerState
    {

        public enum PlayerStateEnumerator
        {
            FROZEN,
            FREE
        }

        public enum PlayerActionStateEnumerator
        {
            //SHARED STATES
            NONE,
            DIVING,
            //GROUNDED STATES
            AWAITINGROLLOUT,
            RUNNING,
            IDLE,
            //AIR STATES
            JUMP,
            DOUBLEJUMP,
            TRIPLEJUMP,
            FALL,
            WALLHUG,
            WALLKICK
        }
        public static PlayerState GetStateClass(PlayerActionStateEnumerator state)
        {
            switch(state)
            {
                case PlayerActionStateEnumerator.RUNNING:
                    return new PS_Running();
                case PlayerActionStateEnumerator.IDLE:
                    return new PS_Idle();
                case PlayerActionStateEnumerator.JUMP:
                    return new PS_Jump();
                case PlayerActionStateEnumerator.DOUBLEJUMP:
                    return new PS_DoubleJump();
                case PlayerActionStateEnumerator.TRIPLEJUMP:
                    return new PS_TripleJump();
                case PlayerActionStateEnumerator.FALL:
                    return new PS_Fall();
                case PlayerActionStateEnumerator.WALLHUG:
                    return new PS_Wallhug();
                case PlayerActionStateEnumerator.WALLKICK:
                    return new PS_Wallkick();
                case PlayerActionStateEnumerator.DIVING:
                    return new PS_Dive();
            }

            return null;
        }

        public static PlayerActionStateEnumerator JumpState()
        {
            switch(PlatformerData.JumpIterator)
            {
                case 0:
                    return PlayerActionStateEnumerator.JUMP;
                case 1:
                    return PlayerActionStateEnumerator.DOUBLEJUMP;
                case 2:
                    return PlayerActionStateEnumerator.TRIPLEJUMP;
            }
            return PlayerActionStateEnumerator.JUMP;
            throw new Exception("Jump iterator out of bounds.");
        }
      
    }
}
