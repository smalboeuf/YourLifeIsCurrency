using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    private int _width;
    private int _height;
    private float _cellSize;
    private T[,] _gridTiles;
    private Vector3 _originPosition;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createGridObject)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _gridTiles = new T[width, height];

        for (int x = 0; x < _gridTiles.GetLength(0); x++)
        {
            for (int y = 0; y < _gridTiles.GetLength(1); y++)
            {
                _gridTiles[x, y] = createGridObject(this, x, y);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
    }

    public Vector2Int GetXY(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
        int y = Mathf.FloorToInt((worldPosition - _originPosition).y / _cellSize);

        return new Vector2Int(x, y);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }

    public void SetValue(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridTiles[x, y] = value;
        }
    }

    public void SetValue(Vector3 worldPosition, T value)
    {
        Vector2Int intVector = GetXY(worldPosition);
        SetValue(intVector.x, intVector.y, value);
    }

    public T GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridTiles[x, y];
        }
        else
        {
            return default(T);
        }
    }

    public T GetValue(Vector3 worldPosition)
    {
        Vector2Int intVector = GetXY(worldPosition);
        return GetValue(intVector.x, intVector.y);
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public float GetCellSize()
    {
        return _cellSize;
    }
}
