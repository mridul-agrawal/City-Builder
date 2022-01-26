using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public Transform Ground;
    public Material transparentMaterial;
    private Dictionary<GameObject, Material[]> originalMaterials = new Dictionary<GameObject, Material[]>();

    //public void CreateBuilding(GridStructure grid, Vector3 gridPosition, GameObject buildingPrefab)
    //{
    //    GameObject newStructure = Instantiate(buildingPrefab, Ground.position + gridPosition, Quaternion.identity);
    //    grid.PlaceStructureOnTheGrid(newStructure, gridPosition);
    //}

    public GameObject CreateGhostStructure(Vector3 gridPosition, GameObject buildingPrefab)
    {
        GameObject newStructure = Instantiate(buildingPrefab, Ground.position + gridPosition, Quaternion.identity);
        Color colorToSet = Color.green;
        ModifyStructurePrefabLook(newStructure, colorToSet);
        return newStructure;
    }

    private void ModifyStructurePrefabLook(GameObject newStructure, Color colorToSet)
    {
        foreach (Transform child in newStructure.transform)
        {
            var renderer = child.GetComponent<MeshRenderer>();
            if (originalMaterials.ContainsKey(child.gameObject) == false)
            {
                originalMaterials.Add(child.gameObject, renderer.materials);
            }
            Material[] materialsToSet = new Material[renderer.materials.Length];
            for (int i = 0; i < materialsToSet.Length; i++)
            {
                materialsToSet[i] = transparentMaterial;
                materialsToSet[i].color = colorToSet;
            }
            renderer.materials = materialsToSet;
        }
    }

    public void ConfirmPlacement(IEnumerable<GameObject> structureCollection)
    {
        foreach (var structure in structureCollection)
        {
            foreach (Transform child in structure.transform)
            {
                var renderer = child.GetComponent<MeshRenderer>();
                if (originalMaterials.ContainsKey(child.gameObject))
                {
                    renderer.materials = originalMaterials[child.gameObject];
                }
            }
        }
        originalMaterials.Clear();
    }

    public void CancelPlacement(IEnumerable<GameObject> structureCollection)
    {
        foreach (var structure in structureCollection)
        {
            Destroy(structure);
        }
        originalMaterials.Clear();
    }

    public void RemoveBuilding(Vector3 gridPosition, GridStructure grid)
    {
        var structure = grid.GetStructureFromTheGrid(gridPosition);
        if (structure != null)
        {
            Destroy(structure);
            grid.RemoveStructureFromTheGrid(gridPosition);
        }
    }

}
