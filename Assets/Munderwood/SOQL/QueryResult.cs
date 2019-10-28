namespace Munderwood.SOQL
{
    public class QueryResult
    {
        public void Test()
        {
            QueryBuilder qb = new QueryBuilder();
            qb.Select("test").From("test").Where("test").OrWhere();
        }
    }
}