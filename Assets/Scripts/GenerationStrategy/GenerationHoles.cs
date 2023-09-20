using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;

public class GenerationHoles : GenerationStrategy
{
    public GenerationHoles() : base(2) {}

    public override GenerationList GetGenerationList()
    {
        currentColumn++;
        return new GenerationList();
    }

    public override void Initialize()
    {
        base.Initialize();
        numColumn = UnityEngine.Random.Range(2, 4);
    }

}
