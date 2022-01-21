using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStructure 
{
    private int CellSize;
    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(inputPosition.x / CellSize);
        int z = Mathf.FloorToInt(inputPosition.z / CellSize);
        return new Vector3(x * CellSize, 0, z * CellSize);
    }
}
