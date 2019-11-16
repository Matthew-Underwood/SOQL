using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Munderwood.SOQL;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ScriptableObjectsCollection scriptableObjectsCollection = new ScriptableObjectsCollection();
        scriptableObjectsCollection.AddToGroup("Tiles",ScriptableObject.CreateInstance(typeof(TestTile1)));
        scriptableObjectsCollection.AddToGroup("Tiles",ScriptableObject.CreateInstance(typeof(TestTile2)));
        scriptableObjectsCollection.AddToGroup("Tiles",ScriptableObject.CreateInstance(typeof(TestTile3)));
        QueryBuilder qb = new QueryBuilder(new QueryProcessor(scriptableObjectsCollection));
        List<Dictionary<string,object>> results = qb.Select("name,visibility").
                                                        From("Tiles").
                                                        Where("name", "=","test tile 1").
                                                        Where("visibility",">",1).
                                                        OrWhere("visibility", "=",1).
                                                        Where("name", "=","test tile 3").
                                                        OrWhere("visibility", "=",5).
                                                     Fetch();
        
        foreach (Dictionary<string,object> result in results)
        {
            Debug.Log(result["name"]);
            Debug.Log(result["visibility"]);
        }
    }
}
