using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField] private GameObject Title;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text bestScoreText;
    [SerializeField] private Text distanceText;
    [SerializeField] private Text bestDistanceText;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject returnTitleButton;
    [SerializeField] private GameManager gameManager;
    public void Start()
    {
        startButton.GetComponent<Button>().onClick.AddListener(gameManager.GameStart);
        returnTitleButton.GetComponent<Button>().onClick.AddListener(gameManager.Title);
        retryButton.GetComponent<Button>().onClick.AddListener(gameManager.Retry);
        gameOver.SetActive(false);
    }

    public void DisplayTitle()
    {
        Title.SetActive(true);

    }

    public void HideTitle()
    {
        Title.SetActive(false);
    }

    public void DisplayGameUi()
    {
        
    }
    
    public void HideGameUi()
    {
        
    }
    public void DisplayGameOver()
    {
        gameOver.SetActive(true);
    }
    public void HideGameOver()
    {
        gameOver.SetActive(false);
    }

    public void SetScoreTextForGameOverUi(int score)
    {
        scoreText.text = score.ToString();
    }
    public void SetBestScoreTextForGameOverUi(int score)
    {
        bestScoreText.text = "BEST: " + score;
    }
    
    public void SetDistanceTextForGameOverUi(float distance)
    {
        distanceText.text = distance.ToString("F2");
    }
    public void SetBestDistanceTextForGameOverUi(float distance)
    {
        bestDistanceText.text = "BEST: " + distance.ToString("F2");
    }
}
