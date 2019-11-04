namespace Munderwood.SOQL
{
    public class QueryResult
    {
        public void Test()
        {
            QueryBuilder qb = new QueryBuilder();
            var results = qb.Select("test").From("test").Where("test", ">","test").Fetch();
        }
    }
}