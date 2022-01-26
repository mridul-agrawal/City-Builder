using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public Transform Ground;

    public void CreateBuilding(GridStructure grid, Vector3 gridPosition, GameObject buildingPrefab)
    {
        GameObject newStructure = Instantiate(buildingPrefab, Ground.position + gridPosition, Quaternion.identity);
        grid.PlaceStructureOnTheGrid(newStructure, gridPosition);
    }

    public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    {
        var structure = grid.GetStructureFromTheGrid(gridPosition);
        if (structure != null)
        {
            Destroy(structure);
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
    }

}
