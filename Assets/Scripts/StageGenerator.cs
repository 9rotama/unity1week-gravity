using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;


public class StageGenerator : MonoBehaviour
{

    [SerializeField] Transform playerTransform;

    // ステージオブジェクト一覧
    [SerializeField] StageObject[] stageObjectOverview;

    // key:オブジェクトのタイプ、value:規定のオブジェクトのサイズをしてした配列
    // 配列要素使い回して負荷を下げる
    Dictionary<StageObjectType, StageObject[]> stageObjectsMap = new Dictionary<StageObjectType, StageObject[]>();

    // ステージオブジェクト配列それぞれの再生成するさいの添字
    Dictionary<StageObjectType, long> indexRegeneratingMap = new Dictionary<StageObjectType, long>();

    // 地面の生成間隔
    float groundInterval; 

    // 生成位置の基準値
    int generationBaseValue = -30;

    // Start is called before the first frame update
    void Start()
    {

        // ステージオブジェクトの生成、配列に代入
        foreach(StageObjectType type in Enum.GetValues(typeof(StageObjectType))) {
            stageObjectsMap[type] = new StageObject[50];

            for(var i=0; i<stageObjectsMap[type].Length; i++) {
                stageObjectsMap[type][i] = Instantiate(stageObjectOverview[(int)type]);
            }

            // 添字番号を0に初期化
            indexRegeneratingMap.Add(type, 0);
        }



        // シンプルなブロックのサイズに合わせる
        groundInterval = stageObjectOverview[(int)StageObjectType.SimpleBlock].objectSize.x;

    }



    // Update is called once per frame
    void Update()
    {
        // 生成位置を画面外に調整するようの変数
        float addition = 20.0f;

        int limit = Mathf.FloorToInt(playerTransform.position.x / groundInterval);

        Debug.Log(groundInterval);

        // 基準値に合わせてオブジェクトを生成
        // while文でオブジェクトの抜け穴を防ぐ
        while(generationBaseValue < limit) {
            
            var type = StageObjectType.SimpleBlock;
            Vector2 generationPos = new Vector2(generationBaseValue * groundInterval + addition, stageObjectOverview[(int)type].GetLimitYPos().max /*UnityEngine.Random.Range(0,2) == 0 ? stageObjectOverview[(int)type].GetLimitYPos().max : stageObjectOverview[(int)type].GetLimitYPos().min*/);
            Generate(generationPos, type);

            generationPos = new Vector2(generationBaseValue * groundInterval + addition, UnityEngine.Random.Range(0,2) == 0 ? stageObjectOverview[(int)type].GetLimitYPos().min : stageObjectOverview[(int)type].GetLimitYPos().min);
            Generate(generationPos, type);
            
            var num = UnityEngine.Random.Range(0,20);
            if(num == 0){
                GenerateVerticalDoubleBlock(new Vector2( generationBaseValue * groundInterval + addition, stageObjectOverview[(int)type].GetLimitYPos().max));
            } else if(num == 1) {
                GenerateVerticalTripleBlock(new Vector2( generationBaseValue * groundInterval + addition, stageObjectOverview[(int)type].GetLimitYPos().max));
            }

            ++generationBaseValue;
        }


    }
    void GenerateVerticalTripleBlock(Vector2 pos){
        var type = StageObjectType.SimpleBlock;
        Generate(pos, type);
        Generate(pos + Vector2.up * stageObjectOverview[(int)type].objectSize.y, type);
        Generate(pos + Vector2.up * stageObjectOverview[(int)type].objectSize.y * 2, type);
    }

    void GenerateVerticalDoubleBlock(Vector2 pos){
        var type = StageObjectType.SimpleBlock;
        Generate(pos, type);
        Generate(pos + Vector2.up * stageObjectOverview[(int)type].objectSize.y, type);
    }

    // タイプに応じてゲームオブジェクトをpos座標に生成（再配置）
    void Generate(Vector2 pos, StageObjectType type) 
    {
        long index = indexRegeneratingMap[type]++ % stageObjectsMap[type].Length;
        stageObjectsMap[type][index].transform.position = pos;
    }
}
