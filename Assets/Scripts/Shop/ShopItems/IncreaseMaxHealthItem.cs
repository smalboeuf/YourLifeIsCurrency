using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealthItem : ShopItem, IShopItem
{
    [SerializeField] private int _healthIncrease = 10;

    public void OnPurchase()
    {
        // Do Effect
        Purchase();
        Globals.PlayerController.IncreaseMaxHealth(_healthIncrease);
    }
}
