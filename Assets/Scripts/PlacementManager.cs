using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Transform Ground;

    public void CreateBuilding(GridStructure grid, Vector3 gridPosition)
    {
        GameObject newStructure = Instantiate(buildingPrefab, Ground.position + gridPosition, Quaternion.identity);
        grid.PlaceStructureOnTheGrid(newStructure, gridPosition);
    }
}
