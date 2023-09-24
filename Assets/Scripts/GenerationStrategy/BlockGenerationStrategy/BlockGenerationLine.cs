using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;
using Random = UnityEngine.Random;

public class BlockGenerationLine : GenerationStrategy<BlockGenerationType>
{   
    int nth=1;

    float rotationZ;

    StageObjectType objectType;
    bool hasSplinterPossible;
    
    public BlockGenerationLine() : base(2) {}

    public BlockGenerationLine(int _numColumn, bool _hasSplinterPossible, int _nth,float _rotationZ) : base(_numColumn) {
        numColumn = _numColumn;
        rotationZ = _rotationZ;
        nth = _nth;
        hasSplinterPossible = _hasSplinterPossible;
    }

    public override GenerationList GetGenerationList()
    {
        if(hasSplinterPossible){
            var rand = Random.Range(0,3);
            objectType = (rand != 0) ? StageObjectType.SimpleBlock : Common.GetRandom(Common.GetBlockTypeList());
        } else {
            objectType = StageObjectType.SimpleBlock;
        }


        var list = new GenerationList
        {
            (objectType, nth, rotationZ)
        };

        currentColumn++;

        return list;
    }

    public override void Initialize()
    {
        base.Initialize();
        numColumn = Random.Range(2, 10);
        nth = Random.Range(-5, 5);
        if(nth == 0) {
            nth = 1;
        }
        
    }

}
