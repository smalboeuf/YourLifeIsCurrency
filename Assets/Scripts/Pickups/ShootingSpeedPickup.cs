using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSpeedPickup : MonoBehaviour, IPickup
{
    [SerializeField]

    public void OnPickup()
    {
        print("Picked up!");
    }

}
