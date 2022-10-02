using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Unit, IDie
{
    public int ShootHealthCost = 2;
    public int ShootEnemyHealAmount = 3;
    public int BasicAttackDamage = 3;

    private PlayerMovement _playerMovement;
    private PlayerInputs _playerInputs;
    private ProjectileShooter _projectileShooter;

    // Health
    private Health _health;
    [SerializeField] private PlayerHealthBarUI _playerHealthBarUi;

    [SerializeField] Shop _shop;
    public ShopItem InRangeShopItem;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        _playerMovement = GetComponent<PlayerMovement>();
        _playerInputs = GetComponent<PlayerInputs>();
        _projectileShooter = GetComponent<ProjectileShooter>();
        _health = GetComponent<Health>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Shooting();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            _playerMovement.MoveCharacter(_playerInputs.Inputs);
        }
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
        TakeDamage(ShootHealthCost);
    }

    public void OnHitEnemy()
    {
        _health.Heal(ShootEnemyHealAmount);
        _playerHealthBarUi.UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(ShootHealthCost);
        _playerHealthBarUi.UpdateUI();
    }

    public void OnInteract()
    {
        if (InRangeShopItem != null)
        {
            InRangeShopItem.GetComponent<IShopItem>().OnPurchase();
            _shop.OnPurchase();
            InRangeShopItem = null;

            Globals.EnemySpawner.NextRound();
        }
    }

    public void Die()
    {
        print("Player Died");
    }

    public void IncreaseMaxHealth(int amount)
    {
        _health.IncreaseMaxHealth(amount);
        _playerHealthBarUi.UpdateUI();
    }

    public void UpdateTimeBetweenProjectiles(float amount)
    {
        _projectileShooter.TimeBetweenProjectiles += amount;
    }

    public void IncreaseBaseDamage(int damageAddition, int healthCostAddition)
    {
        BasicAttackDamage += damageAddition;
        ShootHealthCost += healthCostAddition;
    }

    public void IncreaseOnHitHealAmount(int amount)
    {
        ShootEnemyHealAmount += amount;
    }
}
