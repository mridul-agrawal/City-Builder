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
    private BuildingManager buildingManager;
    private int CellSize = 3;
    public int width, length;
    public CameraMovement cameraMovement;

    private PlayerState state;
    public PlayerState State { get => state; }

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerRemoveBuildingState demolishState;

    private void Awake()
    {
        buildingManager = new BuildingManager(CellSize, width, length, placementManager);
        selectionState = new PlayerSelectionState(this, cameraMovement);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        state = selectionState;
        state.EnterState();
    }

    private void Start()
    {
        cameraMovement.SetCameraLimits(0, width, 0, length);
        inputManager = FindObjectsOfType<MonoBehaviour>().OfType<IInputManager>().FirstOrDefault();

        inputManager.AddListenerOnPointerDownEvent(HandleInput);
        inputManager.AddListenerOnPointerSecondChangeEvent(HandleInputCameraPan);
        inputManager.AddListenerOnPointerChangeEvent(HandlePointerChange);
        inputManager.AddListenerOnPointerSecondUpEvent(HandleInputCameraStop);

        uiController.AddListenerOnBuildAreaEvent(StartPlacementMode);
        uiController.AddListenerOnCancleActionEvent(CancelAction);
        uiController.AddListenerOnDemolishActionEvent(StartDemolishMode);
    }

    private void HandleInput(Vector3 mousePosition)
    {
        state.OnInputPointerDown(mousePosition);
        
    }

    private void HandlePointerChange(Vector3 position)
    {
        state.OnInputPointerChange(position);
    }

    private void HandleInputCameraStop()
    {
        state.OnInputPanUp();
    }

    private void HandleInputCameraPan(Vector3 position)
    {
        state.OnInputPanChange(position);
    }

    private void StartPlacementMode()
    {
        TransitionToState(buildingSingleStructureState);
    }

    private void StartDemolishMode()
    {
        TransitionToState(demolishState);
    }

    private void CancelAction()
    {
        state.OnCancel();
    }


    public void TransitionToState(PlayerState newState)
    {
        this.state = newState;
        this.state.EnterState();
    }


}
