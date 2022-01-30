using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    public const string highScoreKey = "HighScore";

    private void Start() 
    {
        int highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = $"High Score: {highScore}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
