namespace Munderwood.SOQL.Comparisons
{
    public class LessThanOperator : IComparisonOperator
    {
        public bool Compare(object valueA, object valueB)
        {
            return (int)valueA < (int)valueB;
        }
    }
}