using System;
using UnityEngine;

public class GridStructure 
{
    private int CellSize;
    Cell[,] grid;
    private int width;
    private int length;

    public GridStructure(int size, int width, int length)
    {
        CellSize = size;
        this.width = width;
        this.length = length;
        grid = new Cell[this.width, this.length];
        for(int row=0; row<grid.GetLength(0); row++)
        {
            for(int col=0; col<grid.GetLength(1); col++)
            {
                grid[row, col] = new Cell();
            }
        }
    }

    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(inputPosition.x / CellSize);
        int z = Mathf.FloorToInt(inputPosition.z / CellSize);
        return new Vector3(x * CellSize, 0, z * CellSize);
    }

    public Vector2Int CalculateGridIndex(Vector3 gridPosition)
    {
        return new Vector2Int((int)gridPosition.x / CellSize, (int)gridPosition.z / CellSize);
    }

    public bool IsCellTaken(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if(CheckIndexValidity(cellIndex))
        {
            return grid[cellIndex.y, cellIndex.x].IsTaken;
        }
        throw new IndexOutOfRangeException("No index " + cellIndex + " in the grid.");
    }

    public void PlaceStructureOnTheGrid(GameObject Structure, Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        if (CheckIndexValidity(cellIndex))
        {
            grid[cellIndex.y, cellIndex.x].SetConstruction(Structure);
        }
    }

    private bool CheckIndexValidity(Vector2Int cellIndex)
    {
        return (cellIndex.x >= 0 && cellIndex.x < grid.GetLength(1) && cellIndex.y >= 0 && cellIndex.y < grid.GetLength(0));
    }

    public GameObject GetStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        return grid[cellIndex.y, cellIndex.x].GetStructure();
    }

    public void RemoveStructureFromTheGrid(Vector3 gridPosition)
    {
        var cellIndex = CalculateGridIndex(gridPosition);
        grid[cellIndex.y, cellIndex.x].RemoveStructure();
    }

}
