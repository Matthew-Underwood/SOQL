namespace Munderwood.SOQL.Instructions
{
    public class WhereInstruction : IQueryInstruction
    {
        protected string _whereConditional;
        
        public WhereInstruction(string whereConditional)
        {
            _whereConditional = whereConditional;
        }
        
        public void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}