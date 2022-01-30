using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private GameObject insect;
    [SerializeField] private Vector2 secondsBetweenAstreoids;
    [SerializeField] private Vector2 forceRange;

    [SerializeField] private float forwardPower = 1f;

    [SerializeField] private ScoreHandler scoreHandler;

    private Camera mainCamera;
    private float timer;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnAstreroid();

            timer += Random.Range(secondsBetweenAstreoids.x, secondsBetweenAstreoids.y);
        }
    }

    private void SpawnAstreroid()
    {
        int side = Random.Range(0,4);
        scoreHandler.UpdateScore();

        Vector2 spawnPoint = Vector2.zero;
        Vector2 direction = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                direction = new Vector2 (forwardPower, Random.Range(-1f, 1f));
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                direction = new Vector2 (-forwardPower, Random.Range(-1f, 1f));
                break;
            case 2:
                spawnPoint.y = 0;
                spawnPoint.x = Random.value;
                direction = new Vector2 (Random.Range(-1f, 1f), forwardPower);
                break;
            case 3:
                spawnPoint.y = 1;
                spawnPoint.x = Random.value;
                direction = new Vector2 (Random.Range(-1f, 1f), -forwardPower);
                break;
        }

        Vector3 spawnWorldPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        spawnWorldPoint.z = 0;

        GameObject enemy = Instantiate(enemyTypes[Random.Range(0, enemyTypes.Length)], spawnWorldPoint, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        
        Rigidbody rb = enemy.GetComponent<Rigidbody>();
        rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
    }

    public void ActivateInsect()
    {
        if (insect.activeSelf) { return; }
        int side = Random.Range(0,4);
        //scoreHandler.UpdateScore(5);

        Vector2 spawnPoint = Vector2.zero;

        switch (side)
        {
            case 0:
                spawnPoint.x = 0;
                spawnPoint.y = Random.value;
                break;
            case 1:
                spawnPoint.x = 1;
                spawnPoint.y = Random.value;
                break;
            case 2:
                spawnPoint.y = 0;
                spawnPoint.x = Random.value;
                break;
            case 3:
                spawnPoint.y = 1;
                spawnPoint.x = Random.value;
                break;
        }

        Vector3 spawnWorldPoint = mainCamera.ViewportToWorldPoint(spawnPoint);
        spawnWorldPoint.z = 0;

        insect.transform.position = spawnWorldPoint;
        insect.SetActive(true);
    }
}
