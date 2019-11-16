using System;
using System.Collections.Generic;
using UnityEngine;

namespace Munderwood.SOQL
{
    public class ScriptableObjectsCollection
    {
        protected Dictionary<string, List<ScriptableObject>> scriptableObjectsGroup = new Dictionary<string,List<ScriptableObject>>();

        public void AddToGroup(string name, ScriptableObject scriptableObject)
        {
            List<ScriptableObject> scriptableObjects;
            if (scriptableObjectsGroup.ContainsKey(name))
            {
                scriptableObjects = scriptableObjectsGroup[name];
                scriptableObjects.Add(scriptableObject);
                return;
            }
            scriptableObjectsGroup.Add(name,new List<ScriptableObject>());
            scriptableObjects = scriptableObjectsGroup[name];
            scriptableObjects.Add(scriptableObject);
        }
        
        public List<ScriptableObject> GetFromGroupName(string name)
        {
            if (scriptableObjectsGroup[name] == null)
            {
                throw new Exception();
            }
            return scriptableObjectsGroup[name];
        }
    }
}