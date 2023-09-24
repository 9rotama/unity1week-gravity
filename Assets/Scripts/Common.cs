using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


public static class Common
{   
    

    public static bool TrueOrFalse()
    {
        return UnityEngine.Random.Range(0, 2) == 0;
    }

    private static List<StageObjectType> GetSelectTypeList(string typeName)
    {
        var list = new List<StageObjectType>();
        foreach(StageObjectType type in Enum.GetValues(typeof(StageObjectType))){
            if(type.ToString().Contains(typeName)){
                list.Add(type);
            }
        }
        
        return list;
    }

    public static List<StageObjectType> GetBlockTypeList()
    {
        return GetSelectTypeList("Block");
    }

    public static List<StageObjectType> GetItemTypeList()
    {
        return GetSelectTypeList("Item");
    }

    public static List<StageObjectType> GetObstacleList()
    {
        return GetSelectTypeList("Obstacle");
    }

    public static T GetRandom<T>( List<T> list)
	{
		return list[ UnityEngine.Random.Range(0, list.Count) ];
	}

    


}
