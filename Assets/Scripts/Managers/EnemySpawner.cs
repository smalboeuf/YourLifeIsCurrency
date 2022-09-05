using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Shop _shop;
    public bool CanSpawnEnemies = false;
    public int RemainingEnemies = 0;

    [SerializeField] GameObject _enemiesParent;
    [SerializeField] List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] List<Transform> _spawnLocations = new List<Transform>();

    public int CurrentRound;
    private int _waveSpawnCurrency;
    private List<GameObject> _enemiesToSpawn = new List<GameObject>();

    private float _waveDuration = 20;
    private float _waveTimer;
    private float _spawnInterval = 10;
    private float _spawnTimer = 0;

    private void Start()
    {
        CurrentRound = 1;
        CreateEnemyWave();
    }

    private void Update()
    {
        if (_spawnTimer <= 0 && CanSpawnEnemies)
        {
            if (_enemiesToSpawn.Count > 0)
            {
                int randomSpawnLocationId = Random.Range(0, _spawnLocations.Count);
                Transform randomSpawnLocation = _spawnLocations[randomSpawnLocationId];
                Instantiate(_enemiesToSpawn[0], randomSpawnLocation.position, Quaternion.identity, _enemiesParent.transform);
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
        CanSpawnEnemies = true;
        CurrentRound++;
        print("Current Round: " + CurrentRound);
        if (CurrentRound % 10 == 0)
        {
            // Boss Round
            CreateBossRound();
        }
        else if (CurrentRound % 5 == 0)
        {
            // Shop Round
            CreateShopRound();
        }
        else
        {
            // Normal Round
            CreateEnemyWave();
        }
    }

    public void EnemyDies()
    {
        RemainingEnemies--;
        if (RemainingEnemies <= 0 && CanSpawnEnemies)
        {
            NextRound();
        }
    }

    public void CreateEnemyWave()
    {
        _waveSpawnCurrency = CurrentRound * 5;
        CreateEnemies();

        _spawnInterval = _waveDuration / _enemiesToSpawn.Count;
        _waveTimer = _waveDuration;
        CanSpawnEnemies = true;
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
        CanSpawnEnemies = false;
        print("Shop Round");
        _shop.gameObject.SetActive(true);
        _shop.StartShopRound();
    }

    private void CreateBossRound()
    {
        print("BossRound");
    }
}
