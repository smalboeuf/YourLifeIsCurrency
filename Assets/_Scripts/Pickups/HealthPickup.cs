using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Pickup, IPickup
{
    // Health pickups will be weak early, but become stronger as you increase your max health
    [SerializeField] private float _percentageOfHealthToHealth = 0.10f;

    public void OnPickup()
    {
        PlayerController playerController = Globals.PlayerController;
        int amountToHeal = Mathf.RoundToInt(playerController.GetMaxHealth() * _percentageOfHealthToHealth);
        playerController.Heal(amountToHeal);
    }
}
