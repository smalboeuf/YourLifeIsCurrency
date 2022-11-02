using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _pickupPrefabs;
    [SerializeField] float _pickupDropPercentage = 0.10f;

    [SerializeField] EnemySpawner _enemySpawner;

    public GameObject RandomPickupDrop()
    {
        if (Random.value <= _pickupDropPercentage)
        {
            GameObject randomPickupPrefab = Helpers.GetRandomListEntry(_pickupPrefabs);
            return randomPickupPrefab;
        }
        else
        {
            return null;
        }
    }

    public void HidePlayerHealthUI()
    {
        print("Hide health");
        Image[] children = Globals.HealthGlobeParent.GetComponentsInChildren<Image>();
        Color newColor;
        foreach (Image child in children)
        {
            newColor = child.color;
            newColor.a = 0f;
            child.color = newColor;
        }
    }

    public void ShowPlayerHealthUI()
    {
        print("Show health");
        Image[] children = Globals.HealthGlobeParent.GetComponentsInChildren<Image>();
        Color newColor;
        for (int i = 1; i < children.Length; i++)
        {
            newColor = children[i].color;
            newColor.a = 1f;
            children[i].color = newColor;
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
