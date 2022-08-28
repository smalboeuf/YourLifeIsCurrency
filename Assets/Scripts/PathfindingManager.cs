using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{
    private Pathfinding _pathfinding;
    // Start is called before the first frame update
    void Start()
    {
        _pathfinding = new Pathfinding(20, 20);
    }
}
