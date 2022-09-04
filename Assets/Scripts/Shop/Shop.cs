using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopItem> ShopItems;
    public List<GameObject> CurrentDisplayedItems;


    public void OnPurchase()
    {
        foreach (var CurrentDisplayedItem in CurrentDisplayedItems)
        {
            CurrentDisplayedItem.SetActive(false);
        }
    }
}
