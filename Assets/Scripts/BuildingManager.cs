using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    GridStructure grid;
    PlacementManager placementManager;
    StructureRepository structureRepository;
    StructureModificationFactory helperFactory;
    StructureModificationHelper helper;

    public BuildingManager(int cellSize, int width, int length, PlacementManager placementManager, StructureRepository structureRepository)
    {
        this.grid = new GridStructure(cellSize, width, length);
        this.placementManager = placementManager;
        this.structureRepository = structureRepository;
        this.helperFactory = new StructureModificationFactory(structureRepository, grid, placementManager);
    }

    public void PrepareBuildingManager(Type classType)
    {
        helper = helperFactory.GetHelper(classType);
    }

    public void PrepareStructureForPlacement(Vector3 inputPosition, string structureName, StructureType structureType)
    {
        helper.PrepareStructureForModification(inputPosition, structureName, structureType);
    }


    public void ConfirmModification()
    {
        helper.ConfirmModifications();
    }

    public void CancelModification()
    {
        helper.CancelModifications();
    }

    public void PrepareStructureForDemolitionAt(Vector3 inputPosition)
    {
        helper.PrepareStructureForModification(inputPosition, "", StructureType.None);
    }

    public GameObject CheckForStructureInDictionary(Vector3 inputPosition)
    {
        Vector3 gridPosition = grid.CalculateGridPosition(inputPosition);
        GameObject structureToReturn = null;
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        if (structureToReturn != null)
            return structureToReturn;
        structureToReturn = helper.AccessStructureInDictionary(gridPosition);
        return structureToReturn;
    }

}