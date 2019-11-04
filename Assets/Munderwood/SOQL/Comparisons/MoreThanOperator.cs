namespace Munderwood.SOQL.Comparisons
{
    public class MoreThanOperator : IComparisonOperator
    {
        public bool Compare(object valueA, object valueB)
        {
            return (int) valueA > (int) valueB;
        }
    }
}