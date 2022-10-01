using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    public float radius;
    Camera mainCamera;
    NavMeshAgent navMesh;
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
        if(Input.GetMouseButton(1)) {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground)) {
                Vector3 Rayhit = new Vector3(hit.point.x, 0, hit.point.z);
				navMesh.SetDestination(MoveToPoint(navMesh, Rayhit, radius));
            }
        }
    }
}
