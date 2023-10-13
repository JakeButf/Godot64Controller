using n64proofofconcept.scripts.player.platformercontroller.states;
using n64proofofconcept.scripts.player.platformercontroller.states.groundstates;
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
            FALL
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
