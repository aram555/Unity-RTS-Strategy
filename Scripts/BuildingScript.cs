using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingScript : MonoBehaviour
{
    public NavMeshSurface[] surface;
    RaycastHit hit;
    public GameObject building;
    public LayerMask ground;
    public bool isTrue;
    public bool isRotate;
    private GameObject moneyManager;
    private MoneyManagerScript moneyScript;
    Camera cam;

    private void Start() {
        cam = Camera.main;
        transform.eulerAngles = new Vector3(0, -180, 0);
        //gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        isTrue = true;
        moneyManager = GameObject.FindGameObjectWithTag("MoneyManager");
        moneyScript = moneyManager.GetComponent<MoneyManagerScript>();
        isRotate = false;
    }
    private void Update()
    {
        if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground)) {
            
            if(Input.GetMouseButtonUp(0) && isRotate == false) {
                if(!isTrue) return;
                isRotate = true;
            }
            if(!isRotate) {
                transform.position = hit.point;
            }
            if(isRotate) {
                // transform.position = transform.position;
                Vector3 direction = hit.point - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                Quaternion lookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotationY, 10 * Time.fixedDeltaTime);

                if(Input.GetMouseButtonDown(0)) {
                    // for (int i = 0; i < surface.Length; i++) 
                    // {
                    //     surface [i].BuildNavMesh();
                    // }
                    Destroy(this.gameObject);
                    Instantiate(building, transform.position, transform.rotation);
                }
            }
        }

        if(Input.GetMouseButton(1)) {
            if(isRotate || !isRotate) {
                if(this.gameObject.name == "BarracksBluePrint(Clone)") moneyScript.Money += 150;
                if(this.gameObject.name == "FabrickBluePrint(Clone)") moneyScript.Money += 300;
                if(this.gameObject.name == "ShaxtaBluePrint(Clone)") moneyScript.Money += 250;
                if(this.gameObject.name == "TurretBluePrint(Clone)") moneyScript.Money += 100;
                Destroy(this.gameObject);
            }
        }

        
    }

    public void SpawnBluePrint(GameObject bluePrint) {
        Instantiate(bluePrint, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag != "Ground") {
            isTrue = false;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag != "Ground") {
            isTrue = true;
            gameObject.transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
}
