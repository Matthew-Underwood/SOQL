using System;

namespace Munderwood.SOQL.Comparisons
{
    public class EqualsOperator : IComparisonOperator
    {
        public bool Compare(object valueA, object valueB)
        {
            if (valueA is int && valueB is int)
            {
                int valA = (int) valueA;
                int valB = (int) valueB;
                return valA == valB;
            }
            
            if (valueA is string && valueB is string)
            {
                string valA = (string) valueA;
                string valB = (string) valueB;
                return valA == valB; 
            }
            throw new Exception();
        }
        
    }
}