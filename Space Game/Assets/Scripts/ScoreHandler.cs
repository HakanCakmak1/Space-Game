using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private int firstSpawnScore;
    [SerializeField] private int randomSpawnScore1;
    [SerializeField] private int randomSpawnScore2;

    private int score;
    private int scoreToBeSpawn;

    private void Start() 
    {
        UpdateScoreText();
        scoreToBeSpawn = firstSpawnScore;
    }

    public void UpdateScore()
    {
        score ++;
        UpdateScoreText();
        UpdateSpawnScore();
    }

    public void UpdateScore(int value)
    {
        score += value;
        UpdateScoreText();
        UpdateSpawnScore();
    }

    public int GetScore()
    {
        return score;
    }

    public void DisableText()
    {
        scoreText.enabled = false;
    }

    public void EnableText()
    {
        scoreText.enabled = true;
    }

    private void UpdateScoreText()
    {
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    private void UpdateSpawnScore()
    {
        if (score >= scoreToBeSpawn)
        {
            enemySpawner.ActivateInsect();
            scoreToBeSpawn += Random.Range(randomSpawnScore1, randomSpawnScore2);
        }
    }
}
