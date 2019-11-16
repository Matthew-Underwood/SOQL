using System.Collections.Generic;
using UnityEngine;

namespace Munderwood.SOQL
{
    public class QueryableScriptableObjectCollection
    {
        protected List<ScriptableObject> scriptableObjects;
        public List<ScriptableObject> FilterableScriptableObjects;

        public QueryableScriptableObjectCollection(List<ScriptableObject> scriptableObjects)
        {
            this.FilterableScriptableObjects = scriptableObjects;
            this.scriptableObjects = new List<ScriptableObject>(scriptableObjects);
        }

        public void Reset()
        {
            this.FilterableScriptableObjects = scriptableObjects;
            this.scriptableObjects = new List<ScriptableObject>(scriptableObjects);
        }
    }
}