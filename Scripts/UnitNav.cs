// NULLcode Studio © 2015
// null-code.ru

using UnityEngine;
using System.Collections;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class UnitNav : MonoBehaviour {

	public Vector3 point;
	public float stopDistance = 10;
	private NavMeshAgent nav;

	void Start () 
	{
		nav = GetComponent<NavMeshAgent>();
		point = transform.position;
	}

	void Update () 
	{
		nav.SetDestination(point);

		if(!nav.hasPath)
		{
			if(FillEntry.unitLook) transform.rotation = Quaternion.Lerp(transform.rotation, FillEntry.unitRot, 3 * Time.deltaTime);
		}
		else
		{
			if(nav.remainingDistance < stopDistance && !FillEntry.unitLook)
			{
				float speed = nav.speed / (nav.speed - 0.1f);
				if(nav.velocity.magnitude < speed) point = transform.position;
			}
		}
	}
}
