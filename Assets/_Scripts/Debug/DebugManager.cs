using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugManager : MonoBehaviour
{
    public List<GameObject> Mobs = new List<GameObject>();
    public List<GameObject> Bosses = new List<GameObject>();

    [SerializeField] private GameObject _enemyListParent;
    [SerializeField] private GameObject _bossListParent;
    [SerializeField] private GameObject _spawnMobButtonPrefab;
    [SerializeField] private Transform _enemySpawnpoint;
    [SerializeField] private Transform _mobsParent;
    [SerializeField] private Transform _bossesParent;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateMobLists();
    }

    private void InstantiateMobLists()
    {
        foreach (var mob in Mobs)
        {
            GameObject newButton = Instantiate(_spawnMobButtonPrefab, _spawnMobButtonPrefab.transform.position, Quaternion.identity, _enemyListParent.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = mob.name;
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject createdMob = Instantiate(mob, _enemySpawnpoint.position, Quaternion.identity, _mobsParent);
                createdMob.name = mob.name;
            });
        }

        foreach (var boss in Bosses)
        {
            GameObject newButton = Instantiate(_spawnMobButtonPrefab, _spawnMobButtonPrefab.transform.position, Quaternion.identity, _bossListParent.transform);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = boss.name;
            newButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameObject createdBoss = Instantiate(boss, _enemySpawnpoint.position, Quaternion.identity, _bossesParent);
                createdBoss.name = boss.name;
            });
        }
    }
}
