using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using GenerationList = System.Collections.Generic.List<(StageObjectType type, int nth, float rotationZ)>;

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

    // ブロック生成アルゴリズムを格納
    Dictionary<BlockGenerationType, GenerationStrategy<BlockGenerationType>> blockGenerationStrategyMap = new Dictionary<BlockGenerationType, GenerationStrategy<BlockGenerationType>>();
    
    //
    Dictionary<StarGenerationType, GenerationStrategy<StarGenerationType>> starGenerationStrategyMap = new Dictionary<StarGenerationType, GenerationStrategy<StarGenerationType>>();


    // 地面の生成間隔
    const float groundInterval = 1.3f; 

    // 生成位置の基準値
    int generationBaseValue = -30;

    // 生成タイプ
    BlockGenerationType blockGenerationType;
    StarGenerationType starGenerationType;



    GenerationList prevList = new GenerationList();

    // Start is called before the first frame update
    void Start()
    {

        // ステージオブジェクトの生成、配列に代入。生成個数の決定
        stageObjectsMap[StageObjectType.SimpleBlock] = new StageObject[100];
        stageObjectsMap[StageObjectType.SplinterUpBlock] = new StageObject[50];
        stageObjectsMap[StageObjectType.SplinterUpDownBlock] = new StageObject[20];
        stageObjectsMap[StageObjectType.SplinterUpDownRightBlock] = new StageObject[20];
        stageObjectsMap[StageObjectType.SplinterAllBlock] = new StageObject[20];
        stageObjectsMap[StageObjectType.SmallStarItem] = new StageObject[20];
        stageObjectsMap[StageObjectType.MiddleStarItem] = new StageObject[20];
        stageObjectsMap[StageObjectType.LargeStarItem] = new StageObject[20];
        stageObjectsMap[StageObjectType.TouchGameOver] = new StageObject[10];
        

        foreach(StageObjectType type in Enum.GetValues(typeof(StageObjectType))) {

            for(var i=0; i<stageObjectsMap[type].Length; i++) {
                stageObjectsMap[type][i] = Instantiate(stageObjectOverview[(int)type]);
            }

            // 添字番号を0に初期化
            indexRegeneratingMap.Add(type, 0);
        }

        // ステージの生成方法の初期化
        blockGenerationStrategyMap.Add(BlockGenerationType.Parallel, new BlockGenerationParallel());
        blockGenerationStrategyMap.Add(BlockGenerationType.Stairs, new BlockGenerationStairs());
        blockGenerationStrategyMap.Add(BlockGenerationType.Line, new BlockGenerationLine());

        starGenerationStrategyMap.Add(StarGenerationType.Nothing, new StarGenerationNothing());
        starGenerationStrategyMap.Add(StarGenerationType.Straight, new StarGenerationStraight());
        starGenerationStrategyMap.Add(StarGenerationType.Zigzag, new StarGenerationZigzag());



        // シンプルなブロックのサイズに合わせる
        // groundInterval = stageObjectOverview[(int)StageObjectType.SimpleBlock].objectSize.x;

        Initialize();
    }



    // Update is called once per frame
    void Update()
    {
        GenerateStage();
    }

    public void Initialize()
    {
        generationBaseValue = -50;
        foreach(StageObjectType type in Enum.GetValues(typeof(StageObjectType))) {
            for(var i=0; i<stageObjectsMap[type].Length; i++) {
                stageObjectsMap[type][i].transform.position = Vector2.left * 20;
            }
        }

        GenerateStage();
    }


    void GenerateStage()
    {
          // 生成位置を画面外に調整するようの変数
        float addition = 20.0f;

        int limit = Mathf.FloorToInt(playerTransform.position.x / groundInterval);

        // 基準値に合わせてオブジェクトを生成
        // while文でオブジェクトの抜け穴を防ぐ
        while(generationBaseValue < limit) {

            var generationList = blockGenerationStrategyMap[blockGenerationType].GetGenerationList();

            var prevCurrentNthList = GetNthNotContainedInGenerationList(prevList.Concat(generationList).ToList());

            // 詰み回避
            // 前と現在の生成リストの番目が全て埋まっていたら現在のリストを書き換え
            if(prevCurrentNthList.Count == 0){
                generationList = new GenerationList(){
                    (StageObjectType.SimpleBlock, Common.TrueOrFalse() ? 1 : -1 ,0)
                };
            }

            // TouchGameOverの生成
            const int generationProb = 5;
            const int remainingCount = 4;
            if( gameManager.GameState == GameState.Playing
                && prevCurrentNthList.Count >= remainingCount 
                && Random.Range(1, 101) <= generationProb) {
                generationList.Add((
                    StageObjectType.TouchGameOver, Common.GetRandom(prevCurrentNthList), 0
                ));
            }

            prevList = new GenerationList(generationList);

            // アイテムの生成リストも追加
            generationList.AddRange(starGenerationStrategyMap[starGenerationType].GetGenerationList());

            // 生成
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
            
            // 現在の生成方法が終わったら次の生成方法を選択して初期化する
            // ブロック
            var blockGeneration = blockGenerationStrategyMap[blockGenerationType];
            if(blockGeneration.IsFinished()) {

                blockGenerationType = blockGeneration.NextStrategyType();
                blockGeneration.Initialize();

                if(gameManager.GameState == GameState.Ready) {
                    blockGenerationType = BlockGenerationType.Parallel;
                    blockGenerationStrategyMap[blockGenerationType] = new BlockGenerationParallel(_hasSplinter : false);
                }

            }

            // アイテムスター
            var starGeneration = starGenerationStrategyMap[starGenerationType];
            if(starGeneration.IsFinished()) {

                starGenerationType = starGeneration.NextStrategyType();
                starGeneration.Initialize();

                if(gameManager.GameState == GameState.Ready) {
                    starGenerationType = StarGenerationType.Nothing;
                    starGenerationStrategyMap[starGenerationType].Initialize();
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

    // 生成リストに含まれない番目を返す
    List<int> GetNthNotContainedInGenerationList(GenerationList generationList) {
        const int limitBlockSize = 8;
        var nthList = Enumerable.Range(1, limitBlockSize).ToList();
        foreach(var l in generationList){
        var nth = l.nth <= -1 ? l.nth + limitBlockSize + 1 : l.nth;
            nthList.Remove(nth);
        }

        return nthList;
    }
}
