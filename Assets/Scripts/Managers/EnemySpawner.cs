using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool CanSpawnEnemies = true;
    public int RemainingEnemies = 0;

    [SerializeField] List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] List<Transform> _spawnLocations = new List<Transform>();

    public int CurrentRound;
    private int _waveSpawnCurrency;
    private List<GameObject> _enemiesToSpawn = new List<GameObject>();

    private float _waveDuration;
    private float _waveTimer;
    private float _spawnInterval;
    private float _spawnTimer;

    private void Start()
    {
        CurrentRound = 1;
        CreateEnemyWave();
    }

    private void Update()
    {
        if (RemainingEnemies <= 0 && CanSpawnEnemies)
        {
            NextRound();
        }
    }

    private void FixedUpdate()
    {
        if (_spawnTimer <= 0)
        {
            if (_enemiesToSpawn.Count > 0)
            {
                int randomSpawnLocationId = Random.Range(0, _spawnLocations.Count);
                Transform randomSpawnLocation = _spawnLocations[randomSpawnLocationId];
                Instantiate(_enemiesToSpawn[0], randomSpawnLocation.position, Quaternion.identity);
                RemainingEnemies++;
                _enemiesToSpawn.RemoveAt(0);
                _spawnTimer = _spawnInterval;
            }
        }
        else
        {
            _spawnTimer -= Time.fixedDeltaTime;
            _waveTimer -= Time.fixedDeltaTime;
        }
    }

    public void NextRound()
    {
        CurrentRound++;
        if (CurrentRound % 10 == 0)
        {
            // Boss Round

        }
        else if (CurrentRound % 5 == 0)
        {
            // Shop Round

        }
        else
        {
            // Normal Round
            CreateEnemyWave();
        }
    }

    public void CreateEnemyWave()
    {
        _waveSpawnCurrency = CurrentRound * 10;
        CreateEnemies();

        _spawnInterval = _waveDuration / _enemiesToSpawn.Count;
        _waveTimer = _waveDuration;
    }

    public void CreateEnemies()
    {
        List<GameObject> createdEnemies = new List<GameObject>();
        print(_waveSpawnCurrency);
        while (_waveSpawnCurrency > 0)
        {
            int randomEnemyListId = Random.Range(0, _enemies.Count);
            int randomEnemySpawnCost = _enemies[randomEnemyListId].SpawnCost;

            if (_waveSpawnCurrency - randomEnemySpawnCost >= 0)
            {
                createdEnemies.Add(_enemies[randomEnemyListId].GetPrefab());
                _waveSpawnCurrency -= randomEnemySpawnCost;
            }
            else if (CurrentRound <= 0)
            {
                break;
            }
        }

        _enemiesToSpawn.Clear();
        _enemiesToSpawn = createdEnemies;
    }

    private void CreateShopRound()
    {

    }

    private void CreateBossRound()
    {

    }
}
