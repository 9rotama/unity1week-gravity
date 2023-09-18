using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float elapsedTimeFromStart { get; private set; } = 0;
    public GameState GameState { get; set; } = GameState.Ready;

    private void Start()
    {
        GameState = GameState.Playing;
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
}
