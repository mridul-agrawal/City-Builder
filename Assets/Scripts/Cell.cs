using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    GameObject structure = null;
    bool isTaken = false;

    public bool IsTaken { get => isTaken; }

    public void SetConstruction(GameObject structureModel)
    {
        if (structureModel == null) return;
        this.structure = structureModel;
        this.isTaken = true;
    }

    public GameObject GetStructure()
    {
        return structure;
    }

    public void RemoveStructure()
    {
        structure = null;
        isTaken = false;
    }

}
