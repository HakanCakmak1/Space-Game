using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int playerHealth = 1;
    [SerializeField] private GameOverHandler gameOverHandler;

    private int currentHealth;
    private bool playerDied;

    private void OnEnable()
    {
        if (playerDied)
        {
            currentHealth = 1;
        }
        else
        {
            currentHealth = playerHealth;
        }
    }

    private void OnTriggerEnter()
    {
        Crash();
    }

    private void Crash()
    {
        currentHealth--;
        if (currentHealth == 0)
        {
            gameObject.SetActive(false);
            gameOverHandler.EndGame();
            playerDied = true;
        }
    }

}