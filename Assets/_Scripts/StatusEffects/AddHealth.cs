using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealth : StatusEffect, IStatusEffect
{
    // Health pickups will be weak early, but become stronger as you increase your max health
    [SerializeField] private float _percentageOfHealthToHealth = 0.10f;

    public void OnActivateEffect()
    {
        PlayerController playerController = Globals.PlayerController;
        int amountToHeal = Mathf.RoundToInt(playerController.GetMaxHealth() * _percentageOfHealthToHealth);
        playerController.Heal(amountToHeal);
    }

    public void OnFinishEffect()
    {
        // Nothing to do
    }
}
