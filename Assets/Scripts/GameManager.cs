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
    public CameraMovement cameraMovement;

    private bool buildingModeActive = false;

    private void Start()
    {
        cameraMovement.SetCameraLimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();
        grid = new GridStructure(CellSize, width, length);

        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondChangeEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraStop);

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

    private void HandleInputCameraStop()
    {
        cameraMovement.StopCameraMovement();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        if (buildingModeActive == false)
        {
            cameraMovement.MoveCamera(position);
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
