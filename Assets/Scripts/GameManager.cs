using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // References to important components of the game.
    public IInputManager inputManager;
    public UIController uiController;
    public GameObject placementManagerGameObject;
    private IPlacementManager placementManager;
    private BuildingManager buildingManager;
    public CameraMovement cameraMovement;
    public StructureRepository structureRepository;
    public GameObject resourceManagerGameObject;
    private IResourceManager resourceManager;

    // References to Grid property like Layer Mask and cell size.
    public LayerMask inputMask;
    private int CellSize = 3;
    public int width, length;
    
    
    // References to States.
    private PlayerState state;
    public PlayerState State { get => state; }
    public PlayerSelectionState selectionState;
    public PlayerBuildingSingleStructureState buildingSingleStructureState;
    public PlayerBuildingRoadState buildingRoadState;
    public PlayerBuildingZoneState buildingAreaState;
    public PlayerDemolitionState demolishState;

    private void Awake()
    {

#if (UNITY_EDITOR && TEST) || !(UNITY_IOS || UNITY_ANDROID)
        inputManager = gameObject.AddComponent<InputManager>();
#endif
#if (UNITY_IOS || UNITY_ANDROID)

#endif
    }

    private void PrepareStates()
    {
        buildingManager = new BuildingManager(CellSize, width, length, placementManager, structureRepository, resourceManager);
        resourceManager.PrepareResourceManager(buildingManager);
        selectionState = new PlayerSelectionState(this, buildingManager);
        demolishState = new PlayerDemolitionState(this, buildingManager);
        buildingSingleStructureState = new PlayerBuildingSingleStructureState(this, buildingManager);
        buildingAreaState = new PlayerBuildingZoneState(this, buildingManager);
        buildingRoadState = new PlayerBuildingRoadState(this, buildingManager);
        state = selectionState;
        state.EnterState(null);
    }

    private void Start()
    {
        placementManager = placementManagerGameObject.GetComponent<IPlacementManager>();
        resourceManager = resourceManagerGameObject.GetComponent<IResourceManager>();
        PrepareStates();
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
        inputManager.AddListenerOnPointerUpEvent(() => state.OnInputPointerUp());
    }

    private void AssignUIListeners()
    {
        uiController.AddListenerOnBuildAreaEvent((structureName) => state.OnBuildArea(structureName));
        uiController.AddListenerOnBuildSingleStructureEvent((structureName) => state.OnBuildSingleStructure(structureName));
        uiController.AddListenerOnBuildRoadEvent((structureName) => state.OnBuildRoad(structureName));
        uiController.AddListenerOnCancleActionEvent(() => state.OnCancel());
        uiController.AddListenerOnDemolishActionEvent(() => state.OnDemolishAction());
        uiController.AddListenerOnConfirmActionEvent(() => state.OnConfirmAction());
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
