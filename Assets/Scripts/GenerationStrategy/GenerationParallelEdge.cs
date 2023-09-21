using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;

public class GenerationParallelEdge : GenerationStrategy
{
    bool hasSplinter;

    GenerationLine topLine = new GenerationLine();
    GenerationLine bottomLine = new GenerationLine();


    public GenerationParallelEdge() : base(3) {}

    public GenerationParallelEdge(bool _hasSplinter) : base(3) {
        hasSplinter = _hasSplinter;
        topLine = new GenerationLine(numColumn, false, 1, 0);
        bottomLine = new GenerationLine(numColumn, false, -1, 0);
    }

    public override GenerationList GetGenerationList()
    {     
        GenerationList list = new GenerationList();

        list.AddRange(topLine.GetGenerationList());
        list.AddRange(bottomLine.GetGenerationList());

        currentColumn++;
        return list;
    }

    public override void Initialize()
    {
        base.Initialize();
        numColumn = UnityEngine.Random.Range(5, 10);

        topLine = new GenerationLine(numColumn, Common.TrueOrFalse(), 1, 180f);

        bottomLine = new GenerationLine(numColumn, Common.TrueOrFalse(), -1, 0);
    }


}
