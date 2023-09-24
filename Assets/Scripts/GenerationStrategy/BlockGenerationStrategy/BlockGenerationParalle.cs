using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using Random = UnityEngine.Random;
using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;
using Unity.Burst.Intrinsics;

public class BlockGenerationParallel : GenerationStrategy<BlockGenerationType>
{

    BlockGenerationLine topLine = new BlockGenerationLine();
    BlockGenerationLine bottomLine = new BlockGenerationLine();


    public BlockGenerationParallel() : base(3) {}

    public BlockGenerationParallel(bool _hasSplinter) : base(3) {
        topLine = new BlockGenerationLine(numColumn, _hasSplinter, 1, 0);
        bottomLine = new BlockGenerationLine(numColumn, _hasSplinter, -1, 0);
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
        numColumn = Random.Range(3, 7);

        bool hasSplinter = Common.TrueOrFalse();
        var topNth = 1;
        var bottomNth = -1;



        topLine = new BlockGenerationLine(numColumn, hasSplinter, topNth, 180f);
        bottomLine = new BlockGenerationLine(numColumn, !hasSplinter, bottomNth, 0);
    }


}
