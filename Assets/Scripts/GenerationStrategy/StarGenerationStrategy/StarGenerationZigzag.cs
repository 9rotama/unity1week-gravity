using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;
using Random = UnityEngine.Random;

public class StarGenerationZigzag : GenerationStrategy<StarGenerationType>
{

    int nth=1;
    
    public StarGenerationZigzag() : base(0) {
        Initialize();
    }

    public override GenerationList GetGenerationList()
    {
        currentColumn++;

        return new  GenerationList(){
            (GetRandomStarItemType(), nth+currentColumn % 2, 0)
        };
    }

    public override void Initialize()
    {
        base.Initialize();
        numColumn = Random.Range(3, 5);
        nth = Random.Range(1, 9);
    }

}
