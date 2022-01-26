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
    public LayerMask inputMask;
    private BuildingManager buildingManager;
    private int CellSize = 3;
    public int width, length;
    public CameraMovement cameraMovement;

    private PlayerState state;
    public PlayerState State { get => state; }

    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerRemoveBuildingState demolishState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerBuildingAreaState buildingAreaState;

    private void Awake()
    {
        PrepareStates();
#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(CellSize, width, length, placementManager);
        selectionState = new PlayerSelectionState(this, cameraMovement);
        demolishState = new PlayerRemoveBuildingState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingAreaState = new PlayerBuildingAreaState(this, buildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
        state.EnterState(null);
    }

    private void Start()
    {
        PrepareGameObjects();
        AssignInputListeners();
        AssignUIListeners();
    }

    private void PrepareGameObjects()
    {
        inputManager.MouseInputMask = inputMask;
        cameraMovement.SetCameraLimits(0, width, 0, length);
    }

    private void AssignInputListeners()
    {
        inputManager.AddListenerOnPointerDownEvent((position) => state.OnInputPointerDown(position));
        inputManager.AddListenerOnPointerSecondChangeEvent((position) => state.OnInputPanChange(position));
        inputManager.AddListenerOnPointerSecondUpEvent(() => state.OnInputPanUp());
        inputManager.AddListenerOnPointerChangeEvent((position) => state.OnInputPointerChange(position));
    }

    private void AssignUIListeners()
    {
        uiController.AddListenerOnBuildAreaEvent((structureName) => state.OnBuildArea(structureName));
        uiController.AddListenerOnBuildSingleStructureEvent((structureName) => state.OnBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadEvent((structureName) => state.OnBuildRoad(structureName));
        uiController.AddListenerOnCancleActionEvent(() => state.OnCancel());
        uiController.AddListenerOnDemolishActionEvent(() => state.OnDemolishAction());
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

    private void StartPlacementMode(string variable)
    {
        TransitionToState(buildingSingleStructureState, variable);
    }



    public void TransitionToState(PlayerState newState, string variable)
    {
        this.state = newState;
        this.state.EnterState(variable);
    }


}
