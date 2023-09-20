using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

abstract public class GenerationStrategy
{
    protected int currentColumn = 0;

    protected int numColumn;

    public GenerationStrategy(int _numColumn)
    {
        currentColumn = 0;
        numColumn = _numColumn;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns> <summary>
    /// 
    /// </summary>
    /// <param name="type"></param>
    /// <param name="nth">正数：上からn番目、負数：下からn番目</param>
    /// <param name="GetGenerationList("></param>
    abstract public List<(StageObjectType type, int nth, float rotationZ)> GetGenerationList();

    public bool IsFinished()
    {
        return currentColumn >= numColumn;
    }

    public GenerationType NextStrategyType()
    {   


        int EnumNumOfItems = Enum.GetNames(typeof(GenerationType)).Length;

        return (GenerationType)UnityEngine.Random.Range(0, EnumNumOfItems);
        // return GenerationType.Pyramid;
    }

    public virtual void Initialize()
    {
        currentColumn = 0;
    }


}
