using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonNecromancerBoss : Boss
{
    [SerializeField] private List<GameObject> _spawnableMobs = new List<GameObject>();
    [SerializeField] private List<GameObject> _debuffs = new List<GameObject>();

    private float _abilityAttackCooldown = 5;
    private float _abilityAttackCooldownTimer = 0;

    // Update is called once per frame
    void Update()
    {
        AbilityAttackTimer();
    }

    private void DebuffAttack()
    {
        GameObject debuffGameObject = Helpers.GetRandomListEntry(_debuffs);
        IStatusEffect debuff = debuffGameObject.GetComponent<IStatusEffect>();
        debuff.OnActivateEffect();

        StartAttackTimer();
    }

    private void SpawnMonsterAttack()
    {
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
