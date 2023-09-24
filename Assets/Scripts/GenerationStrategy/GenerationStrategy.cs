using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;


abstract public class GenerationStrategy<StrategyType> where StrategyType : Enum
{
    protected int currentColumn = 1;

    protected int numColumn;

    public GenerationStrategy(int _numColumn)
    {
        currentColumn = 1;
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
    abstract public GenerationList GetGenerationList();

    public bool IsFinished()
    {
        return currentColumn > numColumn;
    }

    virtual public StrategyType NextStrategyType()
    {   
        int EnumNumOfItems = Enum.GetNames(typeof(StrategyType)).Length;

        return (StrategyType)(object)UnityEngine.Random.Range(0, EnumNumOfItems);
    }

    public virtual void Initialize()
    {
        currentColumn = 1;
    }

    protected StageObjectType GetRandomStarItemType()
    {
        var starType = StageObjectType.SmallStarItem;

        var rand = UnityEngine.Random.Range(0, 100);

        if(rand <= 60) {
            starType = StageObjectType.SmallStarItem;
        } else if(rand <= 90) {
            starType = StageObjectType.MiddleStarItem;
        } else {
            starType = StageObjectType.LargeStarItem;
        }

        return starType;
    }


}
