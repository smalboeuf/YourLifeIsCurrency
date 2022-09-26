using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarUI : MonoBehaviour
{
    [SerializeField] private Health _playerHealthBar;
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _playerHealthBar.MaxHealth;
        UpdateUI();
    }


    public void UpdateUI()
    {
        _slider.value = _playerHealthBar.CurrentHealth;
    }
}
