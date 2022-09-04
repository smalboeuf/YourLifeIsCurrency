using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMaxHealthItem : ShopItem, IShopItem
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPurchase()
    {
        // Do Effect
        PayCost();
    }
}
