using System;
using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUiController gameUiController;
    [SerializeField] private GameObject player;
    [SerializeField] private StageGenerator stageGenerator;

    public float elapsedTimeFromStart { get; private set; } = 0;
    public GameState GameState = GameState.Ready;
    public int score {get; private set;} = 0;
    
    private void Start()
    {
        BGMManager.Instance.Play(BGMPath.TITLE);
        BGMManager.Instance.FadeIn(BGMPath.TITLE);
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
    }

    public void GameStart()
    {
        BGMManager.Instance.FadeOut(BGMPath.TITLE);
        BGMManager.Instance.Play(BGMPath.PLAY_BGM);
        BGMManager.Instance.FadeIn(BGMPath.PLAY_BGM);
        gameUiController.HideTitle();
        gameUiController.HideGameOver();;
        gameUiController.DisplayGameUi();
        GameState = GameState.Ready;
        player.GetComponent<Player>().Reset();
        stageGenerator.Initialize();
        GameState = GameState.Playing;
    }

    public void playerOutStage()
    {
        SEManager.Instance.Play(SEPath.OUT_STAGE);
        BGMManager.Instance.Stop(BGMPath.PLAY_BGM);
    
        GameState = GameState.GameOver;
        gameUiController.HideTitle();
        gameUiController.HideGameOver();
        gameUiController.DisplayGameOver();
        GameState = GameState.GameOver;

    }
    
    private void UpdateTime()
    {
        elapsedTimeFromStart += Time.deltaTime;
    }

    private void Update()
    {
        Debug.Log(GameState);
        if (GameState == GameState.Playing)
        {
            UpdateTime();
        }
    }

    public void DecreaseScore()
    {
        score -= 100;
        score = Mathf.Max(0, score);
    }
}
