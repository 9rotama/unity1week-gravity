using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;

public class GenerationParallelEdge : GenerationStrategy
{
    public GenerationParallelEdge() : base(20) {}

    public override GenerationList GetGenerationList()
    {
        var random = UnityEngine.Random.Range(0, 10);
        var type = (random == 0) ? StageObjectType.SplinterUpBlock : StageObjectType.SimpleBlock;
       
        GenerationList list = new GenerationList
        {
            (StageObjectType.SimpleBlock, 1, 0),
            (type, -1, 0)
        };

        currentColumn++;
        return list;
    }

    public override void Initialize()
    {
        base.Initialize();
        numColumn = UnityEngine.Random.Range(10, 15);
    }

}
