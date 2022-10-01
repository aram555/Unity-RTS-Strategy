using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : MonoBehaviour
{
    Camera mainCamera;
    NavMeshAgent navMesh;
    public float radius;
    public LayerMask ground;

	void Start () 
	{
		navMesh = GetComponent<NavMeshAgent>();
        mainCamera = Camera.main;
	}

    public static Vector3 MoveToPoint(NavMeshAgent agent, Vector3 center, float radius) {
        var randDIrection = Random.insideUnitSphere * radius;
        randDIrection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randDIrection, out hit, radius, -1);
        return hit.position;
    }

    void Update()
    {
        RaycastHit hit;

        int unitselecteds = UnitSelections.instance.unitSelected.Count;
        if(unitselecteds > 1) {
            if(Input.GetMouseButton(1)) {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground)) {
                    Vector3 Rayhit = new Vector3(hit.point.x, 0, hit.point.z);
                    if(navMesh.enabled) navMesh.SetDestination(MoveToPoint(navMesh, Rayhit, radius));
                }
            }
            radius = (float)unitselecteds / 2f;
        }
        else {
            if(Input.GetMouseButton(1)) {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground)) {
                    if(navMesh.enabled) navMesh.SetDestination(hit.point);
                }
            }
        }
    }
}
