using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemySpawner _enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOver()
    {
        _enemySpawner.CanSpawnEnemies = false;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }

        // _gameOverPanel.SetActive(true);
    }

    public void Pause()
    {
        // _pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        // _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        // _gameOverPanel.SetActive(false);
        // SpawnWaveEnemies();
        // _canSpawnEnemies = true;
        // _player.CanMove = true;
        // CurrentRound = 0;
    }

    public void QuitGame()
    {
        print("Game Quit");
        Application.Quit();
    }
}
