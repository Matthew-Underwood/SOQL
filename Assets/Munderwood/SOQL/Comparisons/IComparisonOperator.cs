namespace Munderwood.SOQL.Comparisons
{
    public interface IComparisonOperator
    {
        bool Compare(object valueA,object valueB);
    }
}