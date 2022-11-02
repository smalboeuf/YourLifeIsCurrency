using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Health Health;

    public void Start()
    {
        Health = GetComponent<Health>();
    }
}
