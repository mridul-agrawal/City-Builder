using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public IInputManager inputManager;
    public UIController uiController;
    public PlacementManager placementManager;
    private GridStructure grid;
    private int CellSize = 3;
    public int width, length;

    private bool buildingModeActive = false;

    private void Start()
    {
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        grid = new GridStructure(CellSize, width, length);
        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancleActionEvent(CancelAction);
    }

    private void HandleInput(Vector3 mousePosition)
    {
        var gridPosition = grid.CalculateGridPosition(mousePosition);
        if(buildingModeActive && grid.IsCellTaken(gridPosition) == false)
        {
            placementManager.CreateBuilding(grid, gridPosition);
        }
        
    }

    private void StartPlacementMode()
    {
        buildingModeActive = true;
    }

    private void CancelAction()
    {
        buildingModeActive = false;
    }

}
