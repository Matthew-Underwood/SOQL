using System.Collections.Generic;
using System.Reflection;
using Munderwood.SOQL.Instructions;
using Munderwood.SOQL.Operators;
using UnityEngine;
using AndConditionalOperator = Munderwood.SOQL.Operators.AndConditionalOperator;

namespace Munderwood.SOQL
{
    public class QueryProcessor
    {
        protected FromInstruction fromInstruction;
        protected QueryableScriptableObjectCollection _queryableScriptableObjectCollection;
        protected Dictionary <int,SelectInstruction> selectInstructions = new Dictionary <int,SelectInstruction>();
        protected Dictionary <int,WhereInstruction> whereInstructions = new Dictionary <int,WhereInstruction>();
        protected Dictionary <int,IConditionalOperator> conditionalOperators = new Dictionary <int,IConditionalOperator>();
        protected int selectInstructionIndex = 0;
        protected int whereInstructionIndex = 0;
        protected int condtionalInstructionIndex = 0;
        protected ScriptableObjectsCollection scriptableObjectsCollection;

        public QueryProcessor (ScriptableObjectsCollection scriptableObjectsCollection)
        {
            this.scriptableObjectsCollection = scriptableObjectsCollection;
        }
        
        public void AddSelectInstruction (SelectInstruction instruction)
        {
            selectInstructions.Add(this.selectInstructionIndex,instruction);
            this.selectInstructionIndex++;
        }
        
        public void AddFromInstruction (FromInstruction instruction)
        {
            fromInstruction = instruction;
            _queryableScriptableObjectCollection = new QueryableScriptableObjectCollection(scriptableObjectsCollection.GetFromGroupName(fromInstruction.ScriptableObject));
        }
        
        public void AddWhereInstruction (WhereInstruction instruction)
        {
            whereInstructions.Add(this.whereInstructionIndex,instruction);
            this.whereInstructionIndex++;
        }
        public void AddConditionalOperator (IConditionalOperator conditionalOperator)
        {
            conditionalOperators.Add(this.condtionalInstructionIndex,conditionalOperator);
            this.condtionalInstructionIndex++;
        }

        public List<Dictionary<string,object>> ProcessInstructions()
        {
            bool firstRun = true;
            bool whereState = false;
            
            foreach (KeyValuePair<int,WhereInstruction> whereInstruction in whereInstructions)
            {
                whereState = whereInstruction.Value.Process(ref _queryableScriptableObjectCollection.FilterableScriptableObjects);
                if (whereInstruction.Key == whereInstructions.Count - 1)
                {
                    break;
                }
                int conditionalKey = whereInstruction.Key;
                IConditionalOperator conditionalType = conditionalOperators[conditionalKey];

                // previous where statement is true and is followed by an OR
                if (whereState && conditionalOperators[conditionalKey].GetType() == typeof(OrConditionalOperator))
                {
                    break;
                }

                // previous where statement is false and is followed by an OR
                if (!whereState && conditionalOperators[conditionalKey].GetType() == typeof(OrConditionalOperator))
                {
                    // TODO reset the data back to its original state
                    _queryableScriptableObjectCollection.Reset();
                    continue;
                }

                // previous where statement is false and is followed by an And
                if (!whereState && conditionalOperators[conditionalKey].GetType() == typeof(AndConditionalOperator))
                {
                    continue;
                }
            }

            return BuildQueryResult(_queryableScriptableObjectCollection.FilterableScriptableObjects);
        }

        protected List<Dictionary<string,object>> BuildQueryResult(List<ScriptableObject> scriptableObjects)
        {
            List<Dictionary<string,object>> results = new List<Dictionary<string,object>>(); 
            foreach (ScriptableObject scriptableObject in scriptableObjects)
            {
                Dictionary<string,object> fields = new Dictionary<string, object>();
                foreach (KeyValuePair <int,SelectInstruction> selectInstruction in selectInstructions)
                {
                    FieldInfo field = scriptableObject.GetType().GetField(selectInstruction.Value.Field);
                    object value = field.GetValue(scriptableObject);
                    fields[selectInstruction.Value.Field] = value;
                }
                results.Add(fields);
            }
            return results;
        }
    }
}