using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBasicDamageItem : ShopItem, IShopItem
{
    [SerializeField] int _damageAddition = 1;
    [SerializeField] int _healthCostAddition = 1;

    public void OnPurchase()
    {
        // Increases damage but also cost of each bullet
        Globals.PlayerController.IncreaseBaseDamage(_damageAddition, _healthCostAddition);
    }
}
