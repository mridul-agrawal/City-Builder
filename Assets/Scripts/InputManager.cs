using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public LayerMask mouseInputLayer;


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
                Debug.Log(position);
            }
        }
    }
}
