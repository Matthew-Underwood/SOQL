using System.Collections.Generic;
using UnityEngine;

namespace Munderwood.SOQL.Instructions
{
    public interface IQueryInstruction
    {
        bool Process(ref List<ScriptableObject> scriptableObjects);
    }
}