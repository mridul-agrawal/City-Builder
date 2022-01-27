using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepository structureRepository;
    StructureModificationHelper singleStructurePlacementHelper;
    StructureModificationHelper structureDemolitionHelper;

    public BuildingManager(int cellSize, int width, int length, PlacementManager placementManager, StructureRepository structureRepository)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManager = placementManager;
        this.structureRepository = structureRepository;
        singleStructurePlacementHelper = new SingleStructurePlacementHelper(structureRepository, grid, placementManager);
        structureDemolitionHelper = new StructureDemolitionHelper(structureRepository, grid, placementManager);
    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        singleStructurePlacementHelper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }

    public void ConfirmPlacement()
    {
        singleStructurePlacementHelper.ConfirmModifications();
    }

    public void CancelPlacement()
    {
        singleStructurePlacementHelper.CancelModifications();
    }


    public void PrepareStructureForDemolitionAt(Vector3 inputPosition)
    {
        structureDemolitionHelper.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }

    public void CancleDemolition()
    {
        structureDemolitionHelper.CancelModifications();
    }

    public void ConfirmDemolition()
    {
        structureDemolitionHelper.ConfirmModifications();
    }

    public GameObject CheckForStructureToModifyDictionary(Vector3 inputPostion)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPostion);
        GameObject structureToReturn = null;
        structureToReturn = singleStructurePlacementHelper.AccessStructureInDictionary(gridPosition);
        if (structureToReturn != null)
            return structureToReturn;
        structureToReturn = structureDemolitionHelper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }

}