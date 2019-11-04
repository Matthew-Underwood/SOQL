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
        protected string value;
        protected Dictionary<string,IComparisonOperator> comparisonOperators;
        
        public WhereInstruction(string field,string operand,string value)
        {
            this.field = field;
            this.operand = operand;
            this.value = value;
        }
        
        public bool Process(ref List<ScriptableObject> scriptableObjects)
        {
            bool removed = false;
            foreach (ScriptableObject scriptableObject in scriptableObjects)
            {
                FieldInfo field = scriptableObject.GetType().GetField(this.field);
                object value = field.GetValue(scriptableObject);
                if (!comparisonOperators[this.operand].Compare(value,this.value))
                {
                    removed = scriptableObjects.Remove(scriptableObject);
                }
            }
            return removed;
        }
    }
}