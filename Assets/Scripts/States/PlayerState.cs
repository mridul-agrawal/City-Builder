using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected GameManager gameManager;
    protected CameraMovement cameraMovement;
    public PlayerState(GameManager gameManager)
    {
        this.gameManager = gameManager;
        this.cameraMovement = gameManager.cameraMovement;
    }

    public virtual void OnConfirmAction()
    {

    }

    public virtual void OnInputPointerDown(Vector3 position) { }
    public virtual void OnInputPointerChange(Vector3 position) { }
    public virtual void OnInputPointerUp() { }
    public virtual void EnterState(string variable)
    {

    }
    public virtual void OnBuildArea(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingAreaState, structureName);
    }

    public virtual void OnBuildSingleStructure(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingSingleStructureState, structureName);
    }

    public virtual void OnBuildRoad(string structureName)
    {
        this.gameManager.TransitionToState(this.gameManager.buildingRoadState, structureName);
    }

    public virtual void OnDemolishAction()
    {
        this.gameManager.TransitionToState(this.gameManager.demolishState, null);
    }

    public virtual void OnInputPanChange(Vector3 panPosition)
    {
        cameraMovement.MoveCamera(panPosition);
    }

    public virtual void OnInputPanUp()
    {
        cameraMovement.StopCameraMovement();
    }

    public abstract void OnCancel();

}