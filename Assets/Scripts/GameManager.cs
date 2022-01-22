using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputManager inputManager;
    public PlacementManager placementManager;
    private GridStructure grid;
    private int CellSize = 3;
    public int width, length;

    private void Start()
    {
        grid = new GridStructure(CellSize, width, length);
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
    }

    private void HandleInput(Vector3 mousePosition)
    {
        var gridPosition = grid.CalculateGridPosition(mousePosition);
        if(grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(grid, gridPosition);
        }
        
    }
}
