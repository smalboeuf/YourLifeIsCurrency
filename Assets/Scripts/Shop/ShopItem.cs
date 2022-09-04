using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    private Health _playerInRangeHealth;
    [SerializeField] private int _healthCost = 10;

    public void PayCost()
    {
        if (_playerInRangeHealth != null)
        {
            _playerInRangeHealth.TakeDamage(_healthCost);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.InRangeShopItem = this;
        }
    }
}
