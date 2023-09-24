using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;
using Random = UnityEngine.Random;

public class StarGenerationNothing : GenerationStrategy<StarGenerationType>
{

    
    public StarGenerationNothing() : base(0) {
        Initialize();
    }

    public override GenerationList GetGenerationList()
    {
        currentColumn++;
        return new  GenerationList();
    }

    public override void Initialize()
    {
        base.Initialize();
        numColumn = Random.Range(3, 5);
    }

}
