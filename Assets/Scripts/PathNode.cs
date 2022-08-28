using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    private int _x;
    private int _y;

    public int gCost;
    public int hCost;
    public int fCost;

    public bool IsWalkable;

    public PathNode cameFromNode;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
        IsWalkable = true;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    public int GetX()
    {
        return _x;
    }

    public int GetY()
    {
        return _y;
    }
}
