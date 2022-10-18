using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectDashboardUI : MonoBehaviour
{
    [SerializeField] private GameObject _statusEffectUIPrefab;

    private List<GameObject> _activeStatusEffectUIGameobjects = new List<GameObject>();

    public void AddStatusEffect(Sprite sprite, string statusEffectName)
    {
        GameObject statusEffectGameobject = _statusEffectUIPrefab;
        statusEffectGameobject.GetComponent<Image>().sprite = sprite;
        GameObject createdStatusEffect = Instantiate(statusEffectGameobject, statusEffectGameobject.transform.position, Quaternion.identity, gameObject.transform);
        createdStatusEffect.name = statusEffectName;
        _activeStatusEffectUIGameobjects.Add(createdStatusEffect);
    }

    public void RemoveStatusEffect(string statusEffectName)
    {
        foreach (var activeStatusEffectUIGameobject in _activeStatusEffectUIGameobjects)
        {
            if (activeStatusEffectUIGameobject.name == statusEffectName)
            {
                _activeStatusEffectUIGameobjects.Remove(activeStatusEffectUIGameobject);
                Destroy(activeStatusEffectUIGameobject);
                break;
            }
        }
    }
}
