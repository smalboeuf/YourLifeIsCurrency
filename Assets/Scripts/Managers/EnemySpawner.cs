using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] List<Transform> _spawnLocations = new List<Transform>();

    private int _currentRound;
    private int _waveSpawnCurrency;
    private List<GameObject> _enemiesToSpawn = new List<GameObject>();

    private float _waveDuration;
    private float _waveTimer;
    private float _spawnInterval;
    private float _spawnTimer;

    private bool _canSpawnEnemies = true;

    private void Start()
    {
        CreateWave();
    }

    private void FixedUpdate()
    {
        if (_spawnTimer <= 0)
        {
            int randomSpawnLocationId = Random.Range(0, _spawnLocations.Count);
            Transform randomSpawnLocation = _spawnLocations[randomSpawnLocationId];
            Instantiate(_enemiesToSpawn[0], randomSpawnLocation.position, Quaternion.identity);
            _enemiesToSpawn.RemoveAt(0);
            _spawnTimer = _spawnInterval;
        }
        else
        {
            _spawnTimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void CreateWave()
    {
        _waveSpawnCurrency = _currentRound * 10;
        CreateEnemies();

        _spawnInterval = _waveDuration / _enemiesToSpawn.Count;
        _waveTimer = _waveDuration;
    }

    public void CreateEnemies()
    {
        List<GameObject> createdEnemies = new List<GameObject>();

        while (_waveSpawnCurrency > 0)
        {
            int randomEnemyListId = Random.Range(0, _enemies.Count);
            int randomEnemySpawnCost = _enemies[randomEnemyListId].SpawnCost;

            if (_waveSpawnCurrency - randomEnemySpawnCost >= 0)
            {
                createdEnemies.Add(_enemies[randomEnemyListId].GetPrefab());
                _waveSpawnCurrency -= randomEnemySpawnCost;
            }
            else if (_currentRound <= 0)
            {
                break;
            }

            _enemiesToSpawn.Clear();
            _enemiesToSpawn = createdEnemies;
        }
    }
}
