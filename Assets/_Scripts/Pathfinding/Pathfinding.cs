using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public static Pathfinding Instance { get; private set; }

    private Grid<PathNode> _grid;
    private List<PathNode> _openList;
    private List<PathNode> _closedList;

    public Pathfinding(int width, int height)
    {
        Instance = this;
        _grid = new Grid<PathNode>(width, height, 1f, Vector3.zero, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y));
    }

    private List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = _grid.GetValue(startX, startY);
        PathNode endNode = _grid.GetValue(endX, endY);

        _openList = new List<PathNode> { startNode };
        _closedList = new List<PathNode>();

        for (int x = 0; x < _grid.GetWidth(); x++)
        {
            for (int y = 0; y < _grid.GetHeight(); y++)
            {
                PathNode pathNode = _grid.GetValue(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (_openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(_openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            _openList.Remove(currentNode);
            _closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (_closedList.Contains(neighbourNode)) continue;

                if (!neighbourNode.IsWalkable)
                {
                    _closedList.Add(neighbourNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!_openList.Contains(neighbourNode))
                    {
                        _openList.Add(neighbourNode);
                    }
                }
            }
        }

        // Out of nodes on the openList
        return null;
    }

    public List<Vector3> FindPath(Vector3 startWorldPosition, Vector3 endWorldPosition)
    {
        Vector2Int startWorldPositionInt = _grid.GetXY(startWorldPosition);
        Vector2Int endWorldPositionInt = _grid.GetXY(endWorldPosition);

        List<PathNode> path = FindPath(startWorldPositionInt.x, startWorldPositionInt.y, endWorldPositionInt.x, endWorldPositionInt.y);

        if (path == null)
        {
            return null;
        }
        else
        {
            List<Vector3> vectorPath = new List<Vector3>();
            foreach (PathNode pathNode in path)
            {
                vectorPath.Add(new Vector3(pathNode.GetX(), pathNode.GetY()) * _grid.GetCellSize() + Vector3.one * _grid.GetCellSize() * 0.5f);
            }
            return vectorPath;
        }
    }

    public Grid<PathNode> GetGrid()
    {
        return _grid;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (currentNode.GetX() - 1 >= 0)
        {
            // Left
            neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY()));
            // Left Down
            if (currentNode.GetY() - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.GetX() - 1, currentNode.GetY() - 1));
            }
            // Left Up
            if (currentNode.GetY() + 1 < _grid.GetHeight())
            {
                neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY() + 1));
            }
        }

        if (currentNode.GetX() + 1 < _grid.GetWidth())
        {
            // Right
            neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY()));

            // Right Down
            if (currentNode.GetY() - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.GetX() + 1, currentNode.GetY() - 1));
            }
            // Right Up
            if (currentNode.GetY() - 1 >= 0)
            {
                neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY() - 1));
            }
        }

        // Down
        if (currentNode.GetY() - 1 >= 0)
        {
            neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY()));
        }

        // Up
        if (currentNode.GetY() + 1 < _grid.GetHeight())
        {
            neighbourList.Add(GetNode(currentNode.GetX(), currentNode.GetY() + 1));
        }

        return neighbourList;
    }

    private PathNode GetNode(int x, int y)
    {
        return _grid.GetValue(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();

        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.GetX() - b.GetX());
        int yDistance = Mathf.Abs(a.GetY() - b.GetY());
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodeList)
    {
        PathNode lowestFCostNode = pathNodeList[0];

        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
