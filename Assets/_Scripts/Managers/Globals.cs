using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    // Singleton

    [SerializeField] private PlayerController _playerController;
    public static PlayerController PlayerController;

    [SerializeField] private EnemySpawner _enemySpawner;
    public static EnemySpawner EnemySpawner;

    [SerializeField] private Shop _shop;
    public static Shop Shop;

    private void Awake()
    {
        PlayerController = _playerController;
        EnemySpawner = _enemySpawner;
        Shop = _shop;
    }
}
