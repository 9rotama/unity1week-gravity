using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject score;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject returnTitleButton;

    public void Start()
    {
        gameOver.SetActive(false);
    }
    public void DisplayGameOver()
    {
        gameOver.SetActive(true);
    }
}
