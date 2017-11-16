// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    /**/ // FUNGUS_UNDER_SAIL_MOD
    public interface ILoopCommand {}
    //*/
    /// <summary>
    /// Continuously loop through a block of commands while the condition is true. Use the Break command to force the loop to terminate immediately.
    /// </summary>
    [CommandInfo("Flow", 
                 "While", 
                 "Continuously loop through a block of commands while the condition is true. Use the Break command to force the loop to terminate immediately.")]
    [AddComponentMenu("")]
    /*/ // FUNGUS_UNDER_SAIL_MOD
    public class While : If
    /*/
    public class While : If, ILoopCommand
    //*/
    {
        /**/ // FUNGUS_UNDER_SAIL_MOD
        private int lastLoopFrame_ = -1;
        private int loopCountThisFrame_ = 0;
        private static readonly int BREAK_FRAME_COUNT = 10000;
        private int loopCountTotal_ = 0;
        private static readonly int TOTAL_LOOP_COUNT_WARNING = 10000;
        //*/
        #region Public members

        public override void OnEnter()
        {
            bool execute = true;
            if (variable != null)
            {
                execute = EvaluateCondition();
            }

            // Find next End statement at same indent level
            End endCommand = null;
            for (int i = CommandIndex + 1; i < ParentBlock.CommandList.Count; ++i)
            {
                End command = ParentBlock.CommandList[i] as End;
                
                if (command != null && 
                    command.IndentLevel == indentLevel)
                {
                    endCommand = command;
                    break;
                }
            }

            /**/ // FUNGUS_UNDER_SAIL_MOD
            int currentLoopFrame = Time.frameCount;
            if (lastLoopFrame_ == currentLoopFrame) {
                ++loopCountThisFrame_;
                ++loopCountTotal_;
                
                if (loopCountThisFrame_ == BREAK_FRAME_COUNT) {
                    // Continue at next command after End
                    Continue(endCommand.CommandIndex + 1);
                    
                    throw new System.InvalidOperationException(
                        "Possible endless while loop detected.\n"
                        + "The same while loop has run for "
                        + BREAK_FRAME_COUNT
                        + " loops during one frame:\n"
                        + "Flowchart: " + name + "\n"
                        + "Block: " + ParentBlock.BlockName + "\n"
                        + "Index: " + CommandIndex + "\n"
                    );
                }
                
                if (loopCountTotal_ % TOTAL_LOOP_COUNT_WARNING == 0) {
                    // Continue at next command after End
                    Continue(endCommand.CommandIndex + 1);
                    
                    UnityEngine.Debug.LogError(
                        "Abnormally long while loop (with frame changes).\n"
                        + "The same while loop has been running for "
                        + loopCountTotal_ + " loops.\n"
                        + "Consider waiting more frames between loops for your "
                        + "personal sanity and to improve performance.\n"
                        + "Flowchart: " + name + "\n"
                        + "Block: " + ParentBlock.BlockName + "\n"
                        + "Index: " + CommandIndex + "\n"
                    );
                }
            }
            else {
                lastLoopFrame_ = currentLoopFrame;
                loopCountThisFrame_ = 0;
            }
            //*/
            
            if (execute)
            {
                // Tell the following end command to loop back
                endCommand.Loop = true;
                Continue();
            }
            else
            {
                // Continue at next command after End
                Continue (endCommand.CommandIndex + 1);
            }
        }

        public override bool OpenBlock()
        {
            return true;
        }

        public override Color GetButtonColor()
        {
            return new Color32(253, 253, 150, 255);
        }

        #endregion
    }    
}