using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFasterEffect : StatusEffect, IStatusEffect
{
    [SerializeField] float _shootingSpeedChange = -0.05f;

    public void OnActivateEffect()
    {
        Globals.PlayerController.UpdateTimeBetweenProjectiles(_shootingSpeedChange);
    }

    public void OnFinishEffect()
    {
        Globals.PlayerController.UpdateTimeBetweenProjectiles(-_shootingSpeedChange);
    }
}
