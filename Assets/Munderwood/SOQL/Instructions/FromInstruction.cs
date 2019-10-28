namespace Munderwood.SOQL.Instructions
{
    public class FromInstruction : IQueryInstruction
    {
        protected string _scriptableObject;
        public FromInstruction(string scriptableObject)
        {
            _scriptableObject = scriptableObject;
        }
        
        public void Process()
        {
            throw new System.NotImplementedException();
        }
    }
}