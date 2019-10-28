namespace Munderwood.SOQL.Instructions
{
    public class SelectInstruction : IQueryInstruction
    {
        protected string _field;
        
        public SelectInstruction (string field)
        {
            _field = field;
        }

        public void Process()
        {
            
        }
    }
}