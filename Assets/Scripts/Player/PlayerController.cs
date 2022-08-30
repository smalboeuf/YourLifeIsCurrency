using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDie
{
    public int ShootHealthCost = 2;
    public int ShootEnemyHealAmount = 3;

    private PlayerMovement _playerMovement;
    private PlayerInputs _playerInputs;
    private ProjectileShooter _projectileShooter;

    // Health
    private Health _health;
    [SerializeField] private PlayerHealthBarUI _playerHealthBarUi;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputs = GetComponent<PlayerInputs>();
        _projectileShooter = GetComponent<ProjectileShooter>();
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();
    }

    private void FixedUpdate()
    {
        _playerMovement.MoveCharacter(_playerInputs.Inputs);
    }

    private void Shooting()
    {
        Vector3 shootingInputs = _playerInputs.ShootInputs;
        // When holding down arrows, shoot in that direction
        if (shootingInputs != Vector3.zero)
        {
            _projectileShooter.Shoot(shootingInputs);
        }
    }

    public void OnShoot()
    {
        _health.TakeDamage(ShootHealthCost);
        _playerHealthBarUi.UpdateUI();
    }

    public void OnHitEnemy()
    {
        _health.Heal(ShootEnemyHealAmount);
        _playerHealthBarUi.UpdateUI();
    }

    public void Die()
    {
        print("Player Died");
    }
}
