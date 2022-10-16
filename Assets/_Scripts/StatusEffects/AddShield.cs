using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddShield : StatusEffect, IStatusEffect
{
    // Shields will be strong early, but fall off in the late game
    [SerializeField] private int _shieldValue = 10;

    public void OnActivateEffect()
    {
        Globals.PlayerController.AddShield(_shieldValue);
    }

    public void OnFinishEffect()
    {
        // Nothing to do
    }
}
