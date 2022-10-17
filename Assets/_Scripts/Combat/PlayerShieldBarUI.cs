using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShieldBarUI : MonoBehaviour
{
    [SerializeField] private Shield _playerShield;
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void UpdateUI()
    {
        _image.fillAmount = _playerShield.GetShieldPercentage();
    }
}
