using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickup : MonoBehaviour, IPickup
{
    [SerializeField] float _shootingSpeedChange = -0.05f;

    public void OnPickup()
    {
        print("Picked up!");
        Globals.PlayerController.UpdateTimeBetweenProjectiles(_shootingSpeedChange);
    }
}
