namespace Munderwood.SOQL.Comparisons
{
    public class EqualsOperator : IComparisonOperator
    {
        public bool Compare(object valueA, object valueB)
        {
            return (string) valueA == (string) valueB;
        }
    }
}