using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHealthBarDebuff : StatusEffect, IStatusEffect
{
    public void OnActivateEffect()
    {
        Globals.GameManager.HidePlayerHealthUI();
    }

    public void OnFinishEffect()
    {
        Globals.GameManager.ShowPlayerHealthUI();
    }
}
