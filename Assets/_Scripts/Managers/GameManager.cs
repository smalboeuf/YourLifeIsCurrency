using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _pickupPrefabs;
    [SerializeField] float _pickupDropPercentage = 0.10f;

    [SerializeField] EnemySpawner _enemySpawner;

    public GameObject RandomPickupDrop()
    {
        if (Random.value <= _pickupDropPercentage)
        {
            int randomPickupPrefabId = Random.Range(0, _pickupPrefabs.Count);
            GameObject randomPickupPrefab = _pickupPrefabs[randomPickupPrefabId];

            return randomPickupPrefab;
        }
        else
        {
            return null;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }

    public void DebugMode()
    {
        print("Loading Debug");
        SceneManager.LoadScene(1);
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
