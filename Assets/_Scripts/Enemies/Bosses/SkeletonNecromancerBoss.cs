using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNecromancerBoss : Boss
{
    [SerializeField] private List<GameObject> _spawnableMobs = new List<GameObject>();
    [SerializeField] private List<GameObject> _debuffs = new List<GameObject>();

    private float _abilityAttackCooldown = 5;
    private float _abilityAttackCooldownTimer = 0;

    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Randomly choose one of his following 2 movement actions
        if (_abilityAttackCooldownTimer <= 0)
        {
            if (Random.Range(0, 2) == 0)
            {
                print("debuff attack");
                DebuffAttack();
            }
            else
            {
                print("spawn monster attack");
                SpawnMonsterAttack();
            }
        }

        AbilityAttackTimer();
    }

    private void DebuffAttack()
    {
        // TODO: Create Animation for Debuff Attack
        GameObject debuffGameObject = Helpers.GetRandomListEntry(_debuffs);
        IStatusEffect debuff = debuffGameObject.GetComponent<IStatusEffect>();
        debuff.OnActivateEffect();
        StatusEffect statusEffect = debuffGameObject.GetComponent<StatusEffect>();

        if (statusEffect.AddsBuff)
        {
            Globals.PlayerController.AddStatusEffectUI(new EnabledStatusEffectTracker(statusEffect), null);
        }

        StartAttackTimer();
    }

    private void SpawnMonsterAttack()
    {
        // TODO: Create animation for Spawning monsters
        // Animation
        // Spawn a random 
        GameObject enemy = Helpers.GetRandomListEntry(_spawnableMobs);

        StartAttackTimer();
    }

    private void Movement()
    {
        // Ideally, we want this monster to be staying X amount of of space away from the player due to being a ranged boss
    }

    private void StartAttackTimer()
    {
        _abilityAttackCooldownTimer = _abilityAttackCooldown;
    }

    private void AbilityAttackTimer()
    {
        if (_abilityAttackCooldownTimer > 0)
        {
            _abilityAttackCooldownTimer -= Time.deltaTime;
        }
    }
}
