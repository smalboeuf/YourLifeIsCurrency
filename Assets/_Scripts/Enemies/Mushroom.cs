using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy, IEnemyAttack
{
    public void EnemyAttack()
    {
        // Start Animation

        // End of Animation, explode mushroom and destroy object
        print("Mushroom Attack");
        Destroy(gameObject);
    }
}
