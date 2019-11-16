
using System.Collections.Generic;
using Munderwood.SOQL.Instructions;
using Munderwood.SOQL.Operators;
using AndConditionalOperator = Munderwood.SOQL.Operators.AndConditionalOperator;

namespace Munderwood.SOQL
{
    public class QueryBuilder
    {
        protected QueryProcessor _queryProcessor;
        protected bool startWhereExpression = true;

        public QueryBuilder(QueryProcessor queryProcessor)
        {
            this._queryProcessor = queryProcessor;
        }

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
            if (!this.startWhereExpression)
            {
                _queryProcessor.AddConditionalOperator(new AndConditionalOperator());    
            }

            if (this.startWhereExpression)
            {
                this.startWhereExpression = false;
            }
            return this;
        }
        
        public QueryBuilder Where (string field,string operand,int value)
        {
            _queryProcessor.AddWhereInstruction(new WhereInstruction(field,operand,value));
            if (!this.startWhereExpression)
            {
                _queryProcessor.AddConditionalOperator(new AndConditionalOperator());    
            }

            if (this.startWhereExpression)
            {
                this.startWhereExpression = false;
            }
            return this;
        }

        public QueryBuilder OrWhere (string field,string operand,string value)
        {
            _queryProcessor.AddWhereInstruction(new WhereInstruction(field,operand,value));
            _queryProcessor.AddConditionalOperator(new OrConditionalOperator());
            return this;
        }
        
        public QueryBuilder OrWhere (string field,string operand,int value)
        {
            _queryProcessor.AddWhereInstruction(new WhereInstruction(field,operand,value));
            _queryProcessor.AddConditionalOperator(new OrConditionalOperator());
            return this;
        }

        public List<Dictionary<string,object>> Fetch()
        {
            List<Dictionary<string,object>> results = _queryProcessor.ProcessInstructions();
            
            return results;
        }
    }
}