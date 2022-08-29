using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingManager : MonoBehaviour
{
    private Pathfinding _pathfinding;

    [SerializeField] Tilemap _wallTilemap;

    // Start is called before the first frame update
    void Start()
    {
        _pathfinding = new Pathfinding(50, 22);
        SetWalkableTiles();
    }

    private void SetWalkableTiles()
    {
        Grid<PathNode> grid = Pathfinding.Instance.GetGrid();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                if (_wallTilemap.HasTile(new Vector3Int(x, y)))
                {
                    var existingTile = grid.GetValue(x, y);
                    existingTile.SetIsWalkable(false);
                    grid.SetValue(x, y, existingTile);
                }
            }
        }
    }
}
