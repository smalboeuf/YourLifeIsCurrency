using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledStatusEffectTracker : MonoBehaviour
{
    public delegate void OnFinishEffectDelegate();
    public OnFinishEffectDelegate OnFinishEffect = delegate { };

    public bool Active = false;
    private float _totalTime = 0;
    private float _timeRemaining = 0;

    public EnabledStatusEffectTracker(StatusEffect statusEffect)
    {
        IStatusEffect iStatusEffect = statusEffect.GetComponent<IStatusEffect>();
        OnFinishEffect += iStatusEffect.OnFinishEffect;
        _totalTime = statusEffect.TotalTime;
        _timeRemaining = _totalTime;
        Active = true;
    }

    public void EffectTimer()
    {
        if (Active && _timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
        }
        else if (Active && _timeRemaining <= 0)
        {
            Active = false;
        }
    }
}
