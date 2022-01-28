using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    GameObject structure = null;
    StructureBaseSO structureData;
    bool isTaken = false;

    public bool IsTaken { get => isTaken; }

    public void SetConstruction(GameObject structureModel, StructureBaseSO structureData)
    {
        if (structureModel == null) return;
        this.structure = structureModel;
        this.isTaken = true;
        this.structureData = structureData;
    }

    public GameObject GetStructure()
    {
        return structure;
    }

    public void RemoveStructure()
    {
        structure = null;
        isTaken = false;
        structureData = null;
    }

    public StructureBaseSO GetStructureData()
    {
        return structureData;
    }

}
