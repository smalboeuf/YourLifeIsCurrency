using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : Pickup, IPickup
{
    // Shields will be strong early, but fall off in the late game
    [SerializeField] private int _shieldValue;

    public void OnPickup()
    {

    }
}
