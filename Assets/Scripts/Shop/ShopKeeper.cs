using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speechBubbleText;
    [SerializeField] private Transform _shopKeeperEntranceSpawnLocation;
    [SerializeField] private Transform _shopLocation;

    private Transform _destination;

    private float _speed = 3f;
    public bool IsAtDestination = false;

    private void Update()
    {
        if (!IsAtDestination)
        {
            transform.position = Vector2.MoveTowards(transform.position, _destination.position, _speed * Time.deltaTime);

            if (transform.position == _destination.position)
            {
                IsAtDestination = true;
            }
        }
    }

    public void Spawn()
    {
        _destination = _shopLocation;
        IsAtDestination = false;
    }

    public void Leave()
    {
        _destination = _shopKeeperEntranceSpawnLocation;
        IsAtDestination = false;
    }

    public void GenerateEntranceQuote()
    {

    }

    public void GenerateItemDescription(ShopItem item)
    {

    }
}
