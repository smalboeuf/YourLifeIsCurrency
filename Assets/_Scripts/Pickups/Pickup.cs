using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private StatusEffect _statusEffect;

    private void Start()
    {
        _statusEffect = GetComponent<StatusEffect>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            IStatusEffect statusEffect = _statusEffect.GetComponent<IStatusEffect>();
            statusEffect.OnActivateEffect();
            if (_statusEffect.AddsBuff)
            {
                Globals.PlayerController.EnabledStatusEffects.Add(new EnabledStatusEffectTracker(_statusEffect));
            }

            Destroy(gameObject);
        }
    }
}
