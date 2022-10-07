using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionRadius : MonoBehaviour
{
    [SerializeField] private int _damage;
    private bool _dealtDamage = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !_dealtDamage)
        {
            other.GetComponent<PlayerController>().TakeDamage(_damage);
            _dealtDamage = true;
        }
    }
}
