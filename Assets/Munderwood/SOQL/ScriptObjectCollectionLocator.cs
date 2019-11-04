using System.Collections.Generic;
using UnityEngine;

namespace Munderwood.SOQL
{
    public class ScriptObjectCollectionLocator
    {
        protected Dictionary<string ,Dictionary<string , ScriptableObject>> scriptableObjectColltions = new
            Dictionary<string, Dictionary<string, ScriptableObject>>();
    }
}