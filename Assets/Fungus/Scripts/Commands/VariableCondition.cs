// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

/**/ // SNOWFALL_FUNGUS_MOD
using System.Collections.Generic;
//*/
using UnityEngine;

namespace Fungus
{
    public abstract class VariableCondition : Condition
    {
        [Tooltip("The type of comparison to be performed")]
        [SerializeField] protected CompareOperator compareOperator;

        [Tooltip("Variable to use in expression")]
        /*/ // SNOWFALL_FUNGUS_MOD
        [VariableProperty(typeof(BooleanVariable),
                          typeof(IntegerVariable), 
                          typeof(FloatVariable), 
                          typeof(StringVariable))]
        /*/
        [VariableProperty]
        //*/
        [SerializeField] protected Variable variable;

        [Tooltip("Boolean value to compare against")]
        [SerializeField] protected BooleanData booleanData;

        [Tooltip("Integer value to compare against")]
        [SerializeField] protected IntegerData integerData;

        [Tooltip("Float value to compare against")]
        [SerializeField] protected FloatData floatData;

        [Tooltip("String value to compare against")]
        [SerializeField] protected StringDataMulti stringData;
        
        /**/ // SNOWFALL_FUNGUS_MOD
        // NOTE: List comparable variable types and description lambdas here
        public static Dictionary<System.Type, System.Func<VariableCondition, string>> variableTypesAndDescriptions = new Dictionary<System.Type, System.Func<VariableCondition, string>>{
            // Custom variable types
            {
                typeof(Variable_ActivitySlot),
                (VariableCondition cmd) => { return cmd.activitySlotData.GetDescription(); }
            },
            {
                typeof(Variable_ActivitySlotOwner),
                (VariableCondition cmd) => { return cmd.activitySlotOwnerData.GetDescription(); }
            },
            {
                typeof(Variable_ChallengeTimer),
                (VariableCondition cmd) => { return cmd.challengeTimerData.GetDescription(); }
            },
            {
                typeof(Variable_Character),
                (VariableCondition cmd) => { return cmd.characterData.GetDescription(); }
            },
            {
                typeof(Variable_CharacterId),
                (VariableCondition cmd) => { return cmd.characterIdData.GetDescription(); }
            },
            {
                typeof(Variable_CharacterState),
                (VariableCondition cmd) => { return cmd.characterStateData.GetDescription(); }
            },
            {
                typeof(Variable_Goal),
                (VariableCondition cmd) => { return cmd.goalData.GetDescription(); }
            },
            {
                typeof(Variable_GoalId),
                (VariableCondition cmd) => { return cmd.goalIdData.GetDescription(); }
            },
            {
                typeof(Variable_StateManager_State),
                (VariableCondition cmd) => { return cmd.stateManagerStateData.GetDescription(); }
            },
            
            // Fungus' own variable types (add If command support)
            {
                typeof(GameObjectVariable),
                (VariableCondition cmd) => { return cmd.gameObjectData.GetDescription(); }
            },
            {
                typeof(Vector2Variable),
                (VariableCondition cmd) => { return cmd.vector2Data.GetDescription(); }
            },
            {
                typeof(Vector3Variable),
                (VariableCondition cmd) => { return cmd.vector3Data.GetDescription(); }
            }
        };
        
        // NOTE: List comparable variable data types here
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_ActivitySlot activitySlotData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_ActivitySlotOwner activitySlotOwnerData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_ChallengeTimer challengeTimerData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_Character characterData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_CharacterId characterIdData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_CharacterState characterStateData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_Goal goalData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_GoalId goalIdData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Data_StateManager_State stateManagerStateData;
        
        // Add support for Fungus' own variable types
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected GameObjectData gameObjectData;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Vector2Data vector2Data;
        [Tooltip("Value to compare against")]
        [SerializeField]
        protected Vector3Data vector3Data;
        //*/

        protected override bool EvaluateCondition()
        {
            BooleanVariable booleanVariable = variable as BooleanVariable;
            IntegerVariable integerVariable = variable as IntegerVariable;
            FloatVariable floatVariable = variable as FloatVariable;
            StringVariable stringVariable = variable as StringVariable;
            
            bool condition = false;
            
            if (booleanVariable != null)
            {
                condition = booleanVariable.Evaluate(compareOperator, booleanData.Value);
            }
            else if (integerVariable != null)
            {
                condition = integerVariable.Evaluate(compareOperator, integerData.Value);
            }
            else if (floatVariable != null)
            {
                condition = floatVariable.Evaluate(compareOperator, floatData.Value);
            }
            else if (stringVariable != null)
            {
                condition = stringVariable.Evaluate(compareOperator, stringData.Value);
            }
            /**/ // SNOWFALL_FUNGUS_MOD
            else {
                System.Type variableType = variable.GetType();
                
                // NOTE: Add an if statement for variable data here
                if (variableType == typeof(Variable_ActivitySlot)) {
                    return variable.Compare(compareOperator, activitySlotData.Value);
                }
                else if (variableType == typeof(Variable_ActivitySlotOwner)) {
                    return variable.Compare(compareOperator, activitySlotOwnerData.Value);
                }
                else if (variableType == typeof(Variable_ChallengeTimer)) {
                    return variable.Compare(compareOperator, challengeTimerData.Value);
                }
                else if (variableType == typeof(Variable_Character)) {
                    return variable.Compare(compareOperator, characterData.Value);
                }
                else if (variableType == typeof(Variable_CharacterId)) {
                    return variable.Compare(compareOperator, characterIdData.Value);
                }
                else if (variableType == typeof(Variable_CharacterState)) {
                    return variable.Compare(compareOperator, characterStateData.Value);
                }
                else if (variableType == typeof(Variable_Goal)) {
                    return variable.Compare(compareOperator, goalData.Value);
                }
                else if (variableType == typeof(Variable_GoalId)) {
                    return variable.Compare(compareOperator, goalIdData.Value);
                }
                else if (variableType == typeof(Variable_StateManager_State)) {
                    return variable.Compare(compareOperator, stateManagerStateData.Value);
                }
                else if (variableType == typeof(GameObjectVariable)) {
                    return variable.Compare(compareOperator, gameObjectData.Value);
                }
                else if (variableType == typeof(Vector2Variable)) {
                    return variable.Compare(compareOperator, vector2Data.Value);
                }
                else if (variableType == typeof(Vector3Variable)) {
                    return variable.Compare(compareOperator, vector3Data.Value);
                }
                else {
                    throw new System.InvalidOperationException(
                        "Unsupported variable type in VariableCondition command:\n"
                        + "Command: " + GetType().ToString() + "\n"
                        + "Variable: " + variable.Key
                        + " (" + variable.GetType().ToString() + ")\n"
                        + "Flowchart: " + name + "\n"
                        + "Block: " + ParentBlock.BlockName
                    );
                }
            }
            //*/

            return condition;
        }

        protected override bool HasNeededProperties()
        {
            return (variable != null);
        }

        #region Public members

        public override string GetSummary()
        {
            if (variable == null)
            {
                return "Error: No variable selected";
            }

            string summary = variable.Key + " ";
            summary += Condition.GetOperatorDescription(compareOperator) + " ";

            if (variable.GetType() == typeof(BooleanVariable))
            {
                summary += booleanData.GetDescription();
            }
            else if (variable.GetType() == typeof(IntegerVariable))
            {
                summary += integerData.GetDescription();
            }
            else if (variable.GetType() == typeof(FloatVariable))
            {
                summary += floatData.GetDescription();
            }
            else if (variable.GetType() == typeof(StringVariable))
            {
                /*/ // SNOWFALL_FUNGUS_MOD
                summary += stringData.GetDescription();
                /*/
                summary += "\"" + stringData.GetDescription() + "\"";
                //*/
            }
            /**/ // SNOWFALL_FUNGUS_MOD
            else {
                System.Type variableType = variable.GetType();
                
                if (variableTypesAndDescriptions.ContainsKey(variableType)) {
                    summary += variableTypesAndDescriptions[variableType](this);
                }
                else {
                    return "Error: Unsupported variable type";
                }
            }
            //*/

            return summary;
        }

        public override bool HasReference(Variable variable)
        {
            return (variable == this.variable);
        }

        public override Color GetButtonColor()
        {
            return new Color32(253, 253, 150, 255);
        }

        #endregion
    }
}