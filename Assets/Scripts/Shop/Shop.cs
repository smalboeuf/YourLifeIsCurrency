using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<ShopItem> ShopItems;
    public List<GameObject> CurrentDisplayedItems;
    public List<GameObject> _itemPositions;
    public GameObject ItemsParent;

    public void StartShopRound()
    {
        GenerateShopItems();
    }

    public void OnPurchase()
    {
        ClearExistingDisplayedItems();
        gameObject.SetActive(false);
    }

    private void GenerateShopItems()
    {
        ClearExistingDisplayedItems();

        foreach (var itemPosition in _itemPositions)
        {
            ShopItem randomItem = GetRandomShopItem();
            GameObject newItem = Instantiate(randomItem.ItemPrefab, itemPosition.transform.position, Quaternion.identity, ItemsParent.transform);
            CurrentDisplayedItems.Add(newItem);
        }
    }

    private void ClearExistingDisplayedItems()
    {
        foreach (var item in CurrentDisplayedItems)
        {
            Destroy(item);
        }
    }

    private ShopItem GetRandomShopItem()
    {
        return ShopItems[Random.Range(0, ShopItems.Count)];
    }
}
