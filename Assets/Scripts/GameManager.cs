using System;
using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameUiController gameUiController;
    public float elapsedTimeFromStart { get; private set; } = 0;
    public GameState GameState = GameState.Ready;
    public int score {get; private set;} = 0;
    private void Start()
    {
        // GameState = GameState.Playing;
    }

    public void playerOutStage()
    {
        SEManager.Instance.Play(SEPath.OUT_STAGE);
        BGMManager.Instance.Stop(BGMPath.PLAY_BGM);
    
        GameState = GameState.GameOver;
        gameUiController.DisplayGameOver();
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
        }
    }

    public void DecreaseScore()
    {
        score -= 100;
        score = Mathf.Max(0, score);
    }
}
