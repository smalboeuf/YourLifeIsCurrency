using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseLifestealItem : ShopItem, IShopItem
{
    [SerializeField] private int _lifestealIncrease = 1;

    public void OnPurchase()
    {
        // Do Effect
        Purchase();
        Globals.PlayerController.IncreaseOnHitHealAmount(_lifestealIncrease);
    }
}
