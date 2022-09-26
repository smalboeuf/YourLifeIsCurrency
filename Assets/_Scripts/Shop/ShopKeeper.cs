using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopKeeper : MonoBehaviour
{

    [SerializeField] private Transform _shopKeeperEntranceSpawnLocation;
    [SerializeField] private Transform _shopLocation;

    private Transform _destination;
    public bool IsAtDestination = false;
    private float _destinationBuffer = 1;

    private float _speed = 3f;

    // Speech
    [SerializeField] private GameObject _speechBubbleParent;
    [SerializeField] private TextMeshProUGUI _speechBubbleText;
    [SerializeField] private float _speechTime = 4;
    private float _speechTimer = 0;
    private bool _showingSpeechBubble = false;

    [SerializeField]
    List<string> EntranceQuotes = new List<string>
    {
        "Oh hello young one!"
    };

    [SerializeField]
    List<string> ExitQuotes = new List<string>
    {
        "Good choice kiddo! I'll see you around!",
        "Well I guess I will see you around!"
    };

    public enum QuoteType
    {
        Entrance,
        Exit,
        OnPurchase
    }

    private void Update()
    {
        if (!IsAtDestination)
        {
            transform.position = Vector2.MoveTowards(transform.position, _destination.position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _destination.position) <= _destinationBuffer && _destination == _shopLocation)
            {
                IsAtDestination = true;
                Globals.Shop.GenerateShopItems();
            }
        }

        SpeechTimer();
    }

    public void Spawn()
    {
        _destination = _shopLocation;
        IsAtDestination = false;
        GenerateQuote(QuoteType.Entrance);
    }

    public void Leave()
    {
        _destination = _shopKeeperEntranceSpawnLocation;
        IsAtDestination = false;
        GenerateQuote(QuoteType.Exit);
    }

    public void GenerateQuote(QuoteType quoteType)
    {
        // TODO: Fade in speech bubble
        switch (quoteType)
        {
            case QuoteType.Entrance:
                _speechBubbleText.text = Helpers.GetRandomListEntry(EntranceQuotes);
                break;
            case QuoteType.Exit:
                _speechBubbleText.text = Helpers.GetRandomListEntry(ExitQuotes);
                break;
        }

        _speechBubbleParent.SetActive(true);
        _showingSpeechBubble = true;
        _speechTimer = _speechTime;
    }

    public void GenerateItemDescription(ShopItem item)
    {

    }

    private void SpeechTimer()
    {
        if (_speechTimer > 0 && _showingSpeechBubble)
        {
            _speechTimer -= Time.deltaTime;
        }

        if (_speechTimer <= 0)
        {
            _showingSpeechBubble = false;
            _speechBubbleParent.SetActive(false);
        }
    }
}
