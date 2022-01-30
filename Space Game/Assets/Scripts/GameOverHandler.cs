using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameoverDisplay;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreHandler scoreHandler;
    [SerializeField] private GameObject player;

    private int score;
    private bool isAdWatched;
    private bool isPaused;

    public const string highScoreKey = "HighScore";
    
    private void OnDestroy() 
    {
        SaveScore();
    }

    public void EndGame()
    {
        enemySpawner.enabled = false;
        gameoverDisplay.gameObject.SetActive(true);
        score = scoreHandler.GetScore();
        scoreHandler.DisableText();
        gameOverText.text = $"Game Over\nScore: {score}";
        pauseButton.SetActive(false);
        isPaused = false;
        if (isAdWatched)
        {
            continueButton.interactable = false;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
        SaveScore();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        SaveScore();
    }

    public void Continue()
    {
        if (!isPaused)
        {
            AdManager.Instance.ShowAd(this);
            isAdWatched = true;
            isPaused = false;
        }
        else
        {
            Time.timeScale = 1;
            isPaused = false;
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        continueButton.interactable = true;
        gameoverDisplay.gameObject.SetActive(true);
        scoreHandler.DisableText();
        gameOverText.text = "Game Paused";
        pauseButton.SetActive(false);
        Time.timeScale = 0;
        score = scoreHandler.GetScore();
    }

    public void ContinueGame()
    {
        gameoverDisplay.gameObject.SetActive(false);
        scoreHandler.EnableText();
        pauseButton.SetActive(true);
        if (!isPaused)
        {
            enemySpawner.enabled = true;
            player.gameObject.SetActive(true);
        }
    }

    private void SaveScore()
    {
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        
        if (score > highScore)
        {
            PlayerPrefs.SetInt(highScoreKey, score);
        }
    }
}
