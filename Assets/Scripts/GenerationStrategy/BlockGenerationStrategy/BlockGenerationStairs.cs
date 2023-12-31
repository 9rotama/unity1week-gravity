using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;

public class BlockGenerationStairs : GenerationStrategy<BlockGenerationType>
{   
    // true:上り、false:下り
    bool isUp;

    // true:上底から生成、false:下底から生成
    bool isTop;

    // 幅
    int width = UnityEngine.Random.Range(1, 5);

    bool avoid;
    
    public BlockGenerationStairs() : base(5) {
        Initialize();
    }

    public BlockGenerationStairs(bool _isUp) : base(5) {
        isUp = _isUp;
        Initialize();
    }

    public override GenerationList GetGenerationList()
    {
        var list = new GenerationList();

        if(avoid){
            avoid = false;
            return new GenerationList(){
                (0, Common.TrueOrFalse() ? 1 : -1, 0)
            };
        }
        

        int height = isUp ? currentColumn : numColumn - currentColumn + 1;

        height = Mathf.CeilToInt(height / (float)width);

        int topBottom = isTop ? 1 : -1;

        for(var i=0; i<height; i++) {
            list.Add((StageObjectType.SimpleBlock, topBottom * (i+1), 0));
        }

        currentColumn++;
        return list;
    }

    public override void Initialize()
    {
        base.Initialize();
        isUp = Common.TrueOrFalse();
        isTop = Common.TrueOrFalse();
        numColumn = UnityEngine.Random.Range(1, 6);
        width =  UnityEngine.Random.Range(1, 5);
        numColumn *= width;
        avoid = !isUp;
    }

}
