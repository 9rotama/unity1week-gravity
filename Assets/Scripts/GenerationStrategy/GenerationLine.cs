using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;
using Random = UnityEngine.Random;

public class GenerationLine : GenerationStrategy
{   
    int nth=1;

    float rotationZ;

    StageObjectType objectType;
    bool hasSplinterPossible;
    
    public GenerationLine() : base(2) {}

    public GenerationLine(int _numColumn, bool _hasSplinterPossible, int _nth,float _rotationZ) : base(_numColumn) {
        numColumn = _numColumn;
        rotationZ = _rotationZ;
        nth = _nth;
        hasSplinterPossible = _hasSplinterPossible;
    }

    public override GenerationList GetGenerationList()
    {
        if(hasSplinterPossible){
            var rand = Random.Range(0,3);
            objectType = (rand != 0) ? StageObjectType.SimpleBlock : (StageObjectType)Random.Range(0, Enum.GetNames(typeof(StageObjectType)).Length);
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
        numColumn = Random.Range(3, 10);
        nth = Random.Range(-5, 5);
        if(nth == 0) {
            nth = 1;
        }
        
    }

}
