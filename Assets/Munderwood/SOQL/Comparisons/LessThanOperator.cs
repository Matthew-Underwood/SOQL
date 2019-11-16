namespace Munderwood.SOQL.Comparisons
{
    public class LessThanOperator : IComparisonOperator
    {
        public bool Compare(object valueA, object valueB)
        {
            int valA = (int) valueA;
            int valB = (int) valueB;
            return valA < valB;
        }
    }
}