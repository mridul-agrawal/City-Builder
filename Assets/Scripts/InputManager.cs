using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public LayerMask mouseInputLayer;
    public int CellSize = 3;
    public GameObject buildingPrefab;


    private void Update()
    {
        GetInput();
    }

    public void GetInput()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mouseInputLayer))
            {
                Vector3 position = hit.point - transform.position;
                Debug.Log(CalculateGridPosition(position));
                PlaceBuildingOnGrid(CalculateGridPosition(position));
            }
        }
    }

    public Vector3 CalculateGridPosition(Vector3 inputPosition)
    {
        int x = Mathf.FloorToInt(inputPosition.x / CellSize);
        int z = Mathf.FloorToInt(inputPosition.z / CellSize);
        return new Vector3(x * CellSize, 0, z * CellSize);
    }

    public void PlaceBuildingOnGrid(Vector3 gridPosition)
    {
        Instantiate(buildingPrefab, gridPosition, Quaternion.identity);
    }

}
