using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public GameObject ItemPrefab;
    private Health _playerInRangeHealth;
    [SerializeField] public int HealthCost = 10;
    [SerializeField] public string Description;

    public void Purchase()
    {
        if (_playerInRangeHealth != null)
        {
            _playerInRangeHealth.TakeDamage(HealthCost);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.InRangeShopItem = this;
            Globals.Shop.ShopKeeper.GenerateItemDescription(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Globals.Shop.ShopKeeper.HideItemDescription();
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.InRangeShopItem = null;
        }
    }
}
