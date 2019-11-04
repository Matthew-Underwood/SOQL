
using System.Collections.Generic;
using Munderwood.SOQL.Instructions;
using Munderwood.SOQL.Operators;
using UnityEngine;
using AndConditionalOperator = Munderwood.SOQL.Operators.AndConditionalOperator;

namespace Munderwood.SOQL
{
    public class QueryBuilder
    {
        //TODO need to turn registry into factory
        protected QueryProcessor _queryProcessor = new QueryProcessor(new ScriptableObjectsRegistry());

        public QueryBuilder Select (string value)
        {
            string[] fields = value.Split(',');
            foreach (string field in fields)
            {
                SelectInstruction selectInstruction = new SelectInstruction {Field = field};
                _queryProcessor.AddSelectInstruction(selectInstruction);
            }
            return this;
        }

        public QueryBuilder From (string value)
        {
            FromInstruction fromInstruction = new FromInstruction {ScriptableObject = value};
            _queryProcessor.AddFromInstruction(fromInstruction);
            return this;
        }

        public QueryBuilder Where (string field,string operand,string value)
        {
            _queryProcessor.AddWhereInstruction(new WhereInstruction(field,operand,value));
            _queryProcessor.AddConditionalOperator(new AndConditionalOperator());
            return this;
        }

        public QueryBuilder OrWhere (string field,string operand,string value)
        {
            _queryProcessor.AddWhereInstruction(new WhereInstruction(field,operand,value));
            _queryProcessor.AddConditionalOperator(new OrConditionalOperator());
            return this;
        }

        public List<object> Fetch()
        {
            List<object> results = _queryProcessor.ProcessInstructions();
            
            return results;
        }
    }
}