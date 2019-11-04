using System.Collections.Generic;
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
        protected int condtionalInstructionIndex = 0;
        protected ScriptableObjectsRegistry _scriptableObjectsRegistry;

        public QueryProcessor (ScriptableObjectsRegistry scriptableObjectsRegistry)
        {
            this._scriptableObjectsRegistry = scriptableObjectsRegistry;
        }
        
        public void AddSelectInstruction (SelectInstruction instruction)
        {
            selectInstructions.Add(this.selectInstructionIndex,instruction);
        }
        
        public void AddFromInstruction (FromInstruction instruction)
        {
            fromInstruction = instruction;
            _queryableScriptableObjectCollection = new QueryableScriptableObjectCollection();
            //TODO locator object to add scriptableobject collection
        }
        
        public void AddWhereInstruction (WhereInstruction instruction)
        {
            whereInstructions.Add(this.condtionalInstructionIndex,instruction);
        }
        
        public void AddConditionalOperator (IConditionalOperator conditionalOperator)
        {
            conditionalOperators.Add(this.condtionalInstructionIndex,conditionalOperator);
            this.condtionalInstructionIndex++;
        }

        public List<object> ProcessInstructions()
        {
            bool firstRun = true;
            bool whereState = false;
            bool resetEvaluate = false;
            
            foreach (KeyValuePair<int,WhereInstruction> whereInstruction in whereInstructions)
            {
                int conditionalKey = whereInstruction.Key - 1;
                IConditionalOperator conditionalType = conditionalOperators[conditionalKey];
                if (firstRun || resetEvaluate)
                {
                    whereState = whereInstruction.Value.Process(ref _queryableScriptableObjectCollection.FilterableScriptableObjects);
                    firstRun = false;
                    resetEvaluate = false;
                    continue;
                }
                // previous where statement is true and is followed by an OR
                if (whereState && conditionalOperators[conditionalKey].GetType() == typeof(OrConditionalOperator))
                {
                    break;
                }
                // previous where statement is false and is followed by an OR
                if (!whereState && conditionalOperators[conditionalKey].GetType() == typeof(OrConditionalOperator))
                {
                    // TODO reset the data back to its original state
                    resetEvaluate = true;
                    continue;
                }
                // previous where statement is false and is followed by an And
                if (!whereState && conditionalOperators[conditionalKey].GetType() == typeof(AndConditionalOperator))
                {
                    continue;
                }
                whereState = whereInstruction.Value.Process(ref _queryableScriptableObjectCollection.FilterableScriptableObjects);
            }

            return BuildQueryResult(_queryableScriptableObjectCollection.FilterableScriptableObjects);
        }

        protected List<object> BuildQueryResult(List<ScriptableObject> scriptableObjects)
        {
            List<object> results = new List<object>(); 
            foreach (ScriptableObject scriptableObject in scriptableObjects)
            {
                object[] fields = new object[selectInstructions.Count];
                foreach (KeyValuePair <int,SelectInstruction> selectInstruction in selectInstructions)
                {
                    fields[selectInstruction.Key] = selectInstruction.Value.Field;
                }
                results.Add(fields);
            }
            return results;
        }
    }
}