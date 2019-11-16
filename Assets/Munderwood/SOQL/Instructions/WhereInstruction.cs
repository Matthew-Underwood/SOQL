using System.Collections.Generic;
using System.Reflection;
using Munderwood.SOQL.Comparisons;
using UnityEngine;

namespace Munderwood.SOQL.Instructions
{
    public class WhereInstruction : IQueryInstruction
    {
        protected string field;
        protected string operand;
        protected object value;
        protected Dictionary<string,IComparisonOperator> comparisonOperators = new Dictionary<string, IComparisonOperator>();
        
        public WhereInstruction(string field,string operand,object value)
        {
            this.field = field;
            this.operand = operand;
            this.value = value;
            comparisonOperators.Add("=",new EqualsOperator());
            comparisonOperators.Add(">",new MoreThanOperator());
            comparisonOperators.Add("<",new LessThanOperator());
        }
        
        public bool Process(ref List<ScriptableObject> scriptableObjects)
        {
            List<ScriptableObject> tempScriptableObjects = new List<ScriptableObject>();
            foreach (ScriptableObject scriptableObject in scriptableObjects)
            {
                FieldInfo field = scriptableObject.GetType().GetField(this.field);
                object value = field.GetValue(scriptableObject);
                if (!this.comparisonOperators[this.operand].Compare(value,this.value))
                {
                    tempScriptableObjects.Add(scriptableObject);
                }
            }

            foreach (ScriptableObject tempScriptableObject in tempScriptableObjects)
            {
                scriptableObjects.Remove(tempScriptableObject);
            }

            return scriptableObjects.Count != 0;
        }
    }
}