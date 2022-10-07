using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy, IEnemyAttack
{
    [SerializeField] private GameObject _explosionGameObject;

    public void EnemyAttack()
    {
        // End of Animation, explode mushroom and destroy object
        GetAnimator().SetTrigger("Explosion");
        // Destroy(gameObject);
    }

    public void EnableExplosionRadius()
    {
        CanMove = false;
        _explosionGameObject.SetActive(true);
        print("Mushroom: " + CanMove);
    }

    public void DisableAfterExplosion()
    {
        Destroy(gameObject);
    }
}
