using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitClick : MonoBehaviour
{
    private Camera myCam;
    public LayerMask clickable;
    public LayerMask ground;
    public LayerMask building;
    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // RaycastHit hit;
        // Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

        // if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable)) {
        //     Debug.DrawLine(ray.origin, hit.point, Color.red, 1);
        //     if(hit.collider.gameObject) Debug.Log("Detect!" + hit.collider.gameObject.name);
        // }
            
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = myCam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity, clickable)) {
                if(EventSystem.current.IsPointerOverGameObject()) return;
                Debug.DrawRay(ray.origin, hit.point, Color.red, 10);
                if(Input.GetKey(KeyCode.LeftShift)) {
                    UnitSelections.instance.ShiftClickSelect(hit.collider.gameObject);
                }
                else {
                    UnitSelections.instance.ClickSelect(hit.collider.gameObject);
                }
            } else {
                if(EventSystem.current.IsPointerOverGameObject()) return;
                if(!Input.GetKey(KeyCode.LeftShift)) {
                    UnitSelections.instance.DeselectAll();
                }
            }
        }
    }
}
