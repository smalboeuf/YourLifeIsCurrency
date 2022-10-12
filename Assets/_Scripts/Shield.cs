using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // The shield value maxes out at the players total HP
    private int _currentShieldPoints = 0;

    public int GetCurrentShieldPoints()
    {
        return _currentShieldPoints;
    }

    public void AddShield(int amount)
    {
        _currentShieldPoints += amount;
    }

    public void LoseShield(int amount)
    {
        _currentShieldPoints -= amount;
    }

    public void RemoveShield()
    {
        _currentShieldPoints = 0;
    }
}
