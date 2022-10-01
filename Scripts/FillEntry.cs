// NULLcode Studio © 2015
// null-code.ru

using UnityEngine;
using System.Collections;

public class FillEntry : MonoBehaviour {

	public GameObject[] units;
	public int lineCount = 5;
	public float shift = 5;
	public float activeDistance = 1.5f;
	public Transform arrow;
	public SpriteRenderer arrowSprite;

	private GameObject[,] field;
	private int line_y;
	private Vector2 curHit;
	private float rotAngle;
	private GameObject grid;
	private Vector3 HIT;
	private Vector3 curHIT;
	private RaycastHit hit;

	public static bool unitLook;
	public static Quaternion unitRot;

	void Start () 
	{
		arrowSprite.enabled = false;
		unitLook = false;
	}

	void UnitDestination()
	{
		int id = 0;
		for(int y = 0; y < line_y; y++)
		{
			for(int x = 0; x < lineCount; x++)
			{
				if(id < units.Length)
				{
					units[id].GetComponent<UnitNav>().point = field[x,y].transform.position;
					id++;
				}
			}
		}
	}

	void RotUpdate(Transform target)
	{
		// Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y);
		// Vector3 curTarget = Camera.main.ScreenToWorldPoint(curScreenPoint);
		// Vector3 lookPos = curTarget - target.position;
		// lookPos.y = 0;

		Vector3 direction = hit.point - target.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Quaternion lookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, lookAtRotationY, 10 * Time.fixedDeltaTime);

		// Quaternion rotation = Quaternion.LookRotation(lookPos);
		target.rotation = rotation;
		unitRot = target.rotation;
	}

	void ArrowUpdate()
	{
		if(Input.GetMouseButton(1))
		{
			float dis = Vector2.Distance(HIT, curHIT);

			if(dis > activeDistance)
			{
				if(!grid)
				{
					grid = new GameObject();
					grid.transform.position = HIT;
					arrow.transform.position = HIT;
					SetGrid();
				}

				arrowSprite.enabled = true;
			}

			if(grid)
			{
				RotUpdate(arrow);
				
				RotUpdate(grid.transform);
			}
		}
		else
		{
			arrowSprite.enabled = false;
		}
	}

	void Update () 
	{
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray, out hit))
		{
			curHIT = hit.point;

			if(Input.GetMouseButtonDown(1))
			{
				Destroy(grid);
				unitLook = false;
				float a = units.Length;
				float b = lineCount;
				float tmp = a / b;
				line_y = Mathf.CeilToInt(tmp);
				field = new GameObject[lineCount, line_y];
				HIT = hit.point;
			}
			else if(Input.GetMouseButtonUp(1))
			{
				if(grid)
				{
					unitLook = true;
					UnitDestination();
				}
				else
				{
					foreach(GameObject u in units)
					{
						u.GetComponent<UnitNav>().point = hit.point;
					}
				}
			}
		}

		ArrowUpdate();
	}

	void SetGrid()
	{
		float posX = HIT.x - shift * ((float)lineCount / 2);
		float posZ = HIT.z + shift * ((float)line_y / 2);
		float Xreset = posX;
		for(int y = 0; y < line_y; y++)
		{
			posZ -= shift;
			for(int x = 0; x < lineCount; x++)
			{
				posX += shift;
				field[x,y] = new GameObject();
				field[x,y].transform.position = new Vector3(posX, 0, posZ);
				field[x,y].transform.parent = grid.transform;
			}
			posX = Xreset;
		}
	}
}
