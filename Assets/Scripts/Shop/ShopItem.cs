using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject ItemPrefab;
    private Health _playerInRangeHealth;
    [SerializeField] private int _healthCost = 10;

    [SerializeField] public string ShopDescription;

    public void Purchase()
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.InRangeShopItem = null;
        }
    }
}
