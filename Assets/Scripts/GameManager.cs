using System;
using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;
using unityroom.Api;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUiController gameUiController;
    [SerializeField] private GameObject player;
    [SerializeField] private StageGenerator stageGenerator;

    public float elapsedTimeFromStart { get; private set; } = 0;
    public GameState GameState = GameState.Ready;
    public int score {get; private set;} = 0;
    public int finalScore;

    private float playerStartPos;

    private void Start()
    {
        BGMManager.Instance.Play(BGMPath.TITLE);
        BGMManager.Instance.FadeIn(BGMPath.TITLE);
    }

    private void Init()
    {
        playerStartPos = player.transform.position.x;
        finalScore = 0;
        score = 0;
        elapsedTimeFromStart = 0;
    }

    public void Title()
    {
        BGMManager.Instance.FadeOut(BGMPath.PLAY_BGM);
        BGMManager.Instance.Play(BGMPath.TITLE);
        BGMManager.Instance.FadeIn(BGMPath.TITLE);
        gameUiController.HideGameOver();
        gameUiController.HideGameUi();
        gameUiController.DisplayTitle();
        GameState = GameState.Ready;
        player.GetComponent<Player>().Reset();
        stageGenerator.Initialize();
        Init();
    }

    public void GameStart()
    {
        BGMManager.Instance.FadeOut(BGMPath.TITLE);
        BGMManager.Instance.Play(BGMPath.PLAY_BGM);
        BGMManager.Instance.FadeIn(BGMPath.PLAY_BGM);
        gameUiController.HideTitle();
        gameUiController.HideGameOver();;
        gameUiController.DisplayGameUi();
        GameState = GameState.Playing;
        Init();
    }

    public void Retry()
    {
        BGMManager.Instance.Play(BGMPath.PLAY_BGM);
        BGMManager.Instance.FadeIn(BGMPath.PLAY_BGM);
        gameUiController.HideTitle();
        gameUiController.HideGameOver();;
        gameUiController.DisplayGameUi();
        GameState = GameState.Ready;
        player.GetComponent<Player>().Reset();
        stageGenerator.Initialize();
        GameState = GameState.Playing;
        Init();
    }

    public float calcPlayerDistance()
    {
        return Mathf.Max(player.transform.position.x - playerStartPos, 0);
    }

    public (int score, float distance) getBestValues()
    {
        var score = PlayerPrefs.GetInt("bestScore", 0);
        var distance = PlayerPrefs.GetFloat("bestDistance", 0);
        return (score, distance);
    }
    
    public void SetBestValues(int score, float distance)
    {
        PlayerPrefs.SetInt("bestScore", score);
        PlayerPrefs.SetFloat("bestDistance", distance);

        UnityroomApiClient.Instance.SendScore(1, score, ScoreboardWriteMode.HighScoreDesc);
        UnityroomApiClient.Instance.SendScore(2, distance, ScoreboardWriteMode.HighScoreDesc);
    }

    public void playerOutStage()
    {
        SEManager.Instance.Play(SEPath.OUT_STAGE);
        BGMManager.Instance.Stop(BGMPath.PLAY_BGM);
    
        GameState = GameState.GameOver;
        gameUiController.HideTitle();
        gameUiController.HideGameUi();
        gameUiController.HideGameOver();
        gameUiController.DisplayGameOver();
        GameState = GameState.GameOver;

        var bestValues = getBestValues();
        var distance = calcPlayerDistance();
        
        gameUiController.SetScoreTextForGameOverUi(finalScore);
        gameUiController.SetBestScoreTextForGameOverUi(bestValues.score);
        gameUiController.SetDistanceTextForGameOverUi(distance);
        gameUiController.SetBestDistanceTextForGameOverUi(bestValues.distance);

        SetBestValues(Math.Max(bestValues.score, finalScore), Math.Max(bestValues.distance, distance));
    }

    private void UpdateFinalScore()
    {
        finalScore = score + (int)calcPlayerDistance();
    }
    
    private void UpdateTime()
    {
        elapsedTimeFromStart += Time.deltaTime;
    }

    private void Update()
    {
        if (GameState == GameState.Playing)
        {
            UpdateTime();
            UpdateFinalScore();
        }
    }

    public void IncreaseScore(int point)
    {
        score += point;
        score = Mathf.Max(0, score);
    }
}
