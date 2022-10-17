using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // The shield value maxes out at the players total HP
    private int _currentShieldPoints = 0;
    private Health _playerHealth;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
    }

    public int GetCurrentShieldPoints()
    {
        return _currentShieldPoints;
    }

    public void AddShield(int amount)
    {
        if (_currentShieldPoints + amount > _playerHealth.MaxHealth)
        {
            _currentShieldPoints = _playerHealth.MaxHealth;
        }
        else
        {
            _currentShieldPoints += amount;
        }
    }

    public void LoseShield(int amount)
    {
        if (_currentShieldPoints - amount < 0)
        {
            _currentShieldPoints = 0;
        }
        else
        {
            _currentShieldPoints -= amount;
        }
    }

    public void RemoveShield()
    {
        _currentShieldPoints = 0;
    }

    public float GetShieldPercentage()
    {
        return ((float)_currentShieldPoints / (float)_playerHealth.MaxHealth);
    }
}
