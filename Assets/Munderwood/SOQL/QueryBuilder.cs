using Munderwood.SOQL.Instructions;

namespace Munderwood.SOQL
{
    public class QueryBuilder
    {
        protected QueryResult _queryResult = new QueryResult();
        protected QueryProcessor _queryProcessor = new QueryProcessor();

        public QueryBuilder Select (string value)
        {
            string[] fields = value.Split(',');
            foreach (string field in fields)
            {
                _queryProcessor.AddSelectInstruction(new SelectInstruction(field));
            }
            return this;
        }

        public QueryBuilder From (string value)
        {
            _queryProcessor.AddFromInstruction(new FromInstruction(value));
            return this;
        }

        public QueryBuilder Where (string value)
        {
            return this;
        }

        public QueryBuilder OrWhere ()
        {
            return this;
        }

        public QueryResult Fetch()
        {
            return _queryResult;
        }

        protected string Split (string values)
        {
            
        }
    }
}