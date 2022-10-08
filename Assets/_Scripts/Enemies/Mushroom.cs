using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy, IEnemyAttack
{
    [SerializeField] private GameObject _explosionGameObject;

    public void EnemyAttack()
    {
        // End of Animation, explode mushroom and destroy object
        CanMove = false;
        GetAnimator().SetTrigger("Explosion");
        // Destroy(gameObject);
    }

    public void EnableExplosionRadius()
    {
        _explosionGameObject.SetActive(true);
        print("Mushroom: " + CanMove);
    }

    public void DisableAfterExplosion()
    {
        Destroy(gameObject);
    }
}
