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

    // Shield
    private Shield _shield;
    [SerializeField] private PlayerShieldBarUI _playerShieldBarUi;

    [SerializeField] private StatusEffectDashboardUI _statusEffectDashboardUi;

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
        _shield = GetComponent<Shield>();
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

    public void AddStatusEffectUI(EnabledStatusEffectTracker enabledStatusEffectTracker, Sprite sprite)
    {
        EnabledStatusEffects.Add(enabledStatusEffectTracker);
        _statusEffectDashboardUi.AddStatusEffect(sprite, enabledStatusEffectTracker.Name);
    }

    public void RemoveStatusEffectUI(string statusEffectName)
    {
        _statusEffectDashboardUi.RemoveStatusEffect(statusEffectName);
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
        PayHealth(ShootHealthCost);
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

    public int GetMaxHealth()
    {
        return _health.MaxHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        _health.IncreaseMaxHealth(amount);
        _playerHealthBarUi.UpdateUI();
    }

    public void OnHitEnemy()
    {
        _health.Heal(ShootEnemyHealAmount);
        _playerHealthBarUi.UpdateUI();
    }

    public void PayHealth(int amount)
    {
        _health.TakeDamage(amount);
        _playerHealthBarUi.UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        int currentShieldPoints = _shield.GetCurrentShieldPoints();

        if (currentShieldPoints - damage >= 0)
        {
            _shield.LoseShield(damage);
        }
        else
        {
            int remainder = damage - currentShieldPoints;
            _shield.RemoveShield();
            _health.TakeDamage(remainder);
        }

        _playerShieldBarUi.UpdateUI();
        _playerHealthBarUi.UpdateUI();
    }

    public void Heal(int damage)
    {
        _health.Heal(damage);
        _playerHealthBarUi.UpdateUI();
    }

    public void InitializeShield()
    {
        _playerShieldBarUi.UpdateUI();
    }

    public void AddShield(int amount)
    {
        _shield.AddShield(amount);
        _playerShieldBarUi.UpdateUI();
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
