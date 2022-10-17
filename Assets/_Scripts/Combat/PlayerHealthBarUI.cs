using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Health _playerHealthBar;
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        UpdateUI();
        Globals.PlayerController.InitializeShield();
    }

    public void UpdateUI()
    {
        _image.fillAmount = _playerHealthBar.GetHealthPercentage();
    }
}
