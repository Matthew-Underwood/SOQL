using System.Collections.Generic;
using Munderwood.SOQL.Instructions;

namespace Munderwood.SOQL
{
    public class QueryProcessor
    {
        protected FromInstruction fromInstruction;
        protected List <SelectInstruction> selectInstructions = new List <SelectInstruction>();
        protected List <WhereInstruction> whereInstructions = new List <WhereInstruction>();
        public QueryProcessor()
        {
            
        }
        
        public void AddSelectInstruction (SelectInstruction instruction)
        {
            selectInstructions.Add(instruction);
        }
        
        public void AddFromInstruction (FromInstruction instruction)
        {
            fromInstruction = instruction;
        }
        
        public void AddWhereInstruction (WhereInstruction instruction)
        {
            whereInstructions.Add(instruction);
        }

        public void ProcessInstructions()
        {
            
        }

        protected void ValidateInstruction()
        {
            
        }
    }
}