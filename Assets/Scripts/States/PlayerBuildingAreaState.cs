using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingAreaState : PlayerState
{

    BuildingManager buildingManager;
    string structureName;
    public PlayerBuildingAreaState(GameManager gameManager, BuildingManager buildingManager) : base(gameManager)
    {
        this.buildingManager = buildingManager;
    }

    public override void OnCancel()
    {
        this.gameManager.TransitionToState(this.gameManager.selectionState, null);
    }

    public override void EnterState(string structureName)
    {
        base.EnterState(structureName);
        this.structureName = structureName;
    }

    public override void OnInputPointerDown(Vector3 position)
    {

        buildingManager.PlaceStructureAt(position);
    }

}