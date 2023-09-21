using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;


public class StageGenerator : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] Transform playerTransform;

    // ステージオブジェクト一覧
    [SerializeField] StageObject[] stageObjectOverview;

    // key:オブジェクトのタイプ、value:規定のオブジェクトのサイズをしてした配列
    // 配列要素使い回して負荷を下げる
    Dictionary<StageObjectType, StageObject[]> stageObjectsMap = new Dictionary<StageObjectType, StageObject[]>();

    // ステージオブジェクト配列それぞれの再生成するさいの添字
    Dictionary<StageObjectType, long> indexRegeneratingMap = new Dictionary<StageObjectType, long>();

    // 生成アルゴリズムを格納
    Dictionary<GenerationType, GenerationStrategy> generationStrategyMap = new Dictionary<GenerationType, GenerationStrategy>();

    // 地面の生成間隔
    float groundInterval; 

    // 生成位置の基準値
    int generationBaseValue = -30;

    // 生成タイプ
    GenerationType generationType;

    // Start is called before the first frame update
    void Start()
    {

        // ステージオブジェクトの生成、配列に代入
        stageObjectsMap[StageObjectType.SimpleBlock] = new StageObject[100];
        stageObjectsMap[StageObjectType.SplinterUpBlock] = new StageObject[50];
        stageObjectsMap[StageObjectType.SplinterUpDownBlock] = new StageObject[20];
        stageObjectsMap[StageObjectType.SplinterUpDownRightBlock] = new StageObject[20];
        stageObjectsMap[StageObjectType.SplinterAllBlock] = new StageObject[20];

        foreach(StageObjectType type in Enum.GetValues(typeof(StageObjectType))) {

            for(var i=0; i<stageObjectsMap[type].Length; i++) {
                stageObjectsMap[type][i] = Instantiate(stageObjectOverview[(int)type]);
            }

            // 添字番号を0に初期化
            indexRegeneratingMap.Add(type, 0);
        }

        // ステージの生成方法の初期化
        generationStrategyMap.Add(GenerationType.Holes, new GenerationHoles());
        generationStrategyMap.Add(GenerationType.ParallelEdge, new GenerationParallelEdge());
        generationStrategyMap.Add(GenerationType.Stairs, new GenerationStairs());
        generationStrategyMap.Add(GenerationType.Line, new GenerationLine());


        // シンプルなブロックのサイズに合わせる
        groundInterval = stageObjectOverview[(int)StageObjectType.SimpleBlock].objectSize.x;

        generationBaseValue = -50;
        GenerateStage();
    }



    // Update is called once per frame
    void Update()
    {
        
        GenerateStage();

    }

    void GenerateStage()
    {
          // 生成位置を画面外に調整するようの変数
        float addition = 30.0f;

        int limit = Mathf.FloorToInt(playerTransform.position.x / groundInterval);

        // 基準値に合わせてオブジェクトを生成
        // while文でオブジェクトの抜け穴を防ぐ
        while(generationBaseValue < limit) {

            var generationList = generationStrategyMap[generationType].GetGenerationList();

            foreach(var i in generationList){

                StageObject obj = stageObjectOverview[(int)i.type];

                float yPos;
                if(i.nth >= 1) {
                    yPos = -(Mathf.Abs(i.nth)-1) * obj.objectSize.y + obj.GetLimitYPos().max;
                } else {
                    yPos = (Mathf.Abs(i.nth)-1) * obj.objectSize.y + obj.GetLimitYPos().min;
                }

                float xPos = generationBaseValue * groundInterval + addition;

                Generate(i.type, new Vector2(xPos, yPos), i.rotationZ);
            }
            
            if(generationStrategyMap[generationType].IsFinished()) {

                // 次の生成方法を選択して初期化する
                generationType = generationStrategyMap[generationType].NextStrategyType();
                generationStrategyMap[generationType].Initialize();

                if(gameManager.GameState == GameState.Ready) {
                    generationType = GenerationType.ParallelEdge;
                    generationStrategyMap[generationType] = new GenerationParallelEdge(_hasSplinter : false);
                }

            }
            ++generationBaseValue;
        }

    }

    // タイプに応じてゲームオブジェクトをpos座標に生成（再配置）
    void Generate(StageObjectType type, Vector2 pos, float rotationZ=0) 
    {
        long index = indexRegeneratingMap[type]++ % stageObjectsMap[type].Length;
        stageObjectsMap[type][index].transform.position = pos;
        stageObjectsMap[type][index].transform.rotation = Quaternion.Euler(0, 0, rotationZ);

    }
}
