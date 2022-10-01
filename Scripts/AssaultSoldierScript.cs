using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssaultSoldierScript : MonoBehaviour
{
    public int HP;
    public int distance;
    public float speed;
    [Header("Timer")]
    Camera mainCamera;
    NavMeshAgent navMesh;
    [Header("Targets")]
    public Transform targets;
    public float range;
    BulletScript bul1;
    private Animator anim;
    private bool fortres;
    public bool attack;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fortres = false;
        attack = false;
        // gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    public void UpdateTarget() {
        GameObject[] Enemyes = GameObject.FindGameObjectsWithTag("EnemySoldier");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in Enemyes) {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if(distance < shortestDistance) {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if(nearestEnemy != null && shortestDistance <= range) {
            targets = nearestEnemy.transform;
        }
        else {
            targets = null;
        }
    }

    private void Update() {
        if(targets == null) {
            return;
        }

        if(targets != null) {
            Vector3 direction = targets.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion lookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotationY, 10 * Time.fixedDeltaTime);

            if(navMesh.enabled) {
                navMesh.SetDestination(targets.position);
            }
            else {
                return;
            }
        }

        if(Vector3.Distance(transform.position, targets.position) < 2) {
            attack = true;
        }

        if(Vector3.Distance(transform.position, targets.position) < 2) {
            navMesh.SetDestination(transform.position);
        }

    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("EnemyBullet")) {
            Destroy(other.gameObject);
            Debug.Log("Pow");
            HP -= 2;
        }
        if(other.gameObject.name == "EnemyRocketBullet" || other.gameObject.name == "EnemyRocketBullet(Clone)") {
            Destroy(other.gameObject);
            Debug.Log("Pow");
            HP -= 5;
        }

        if(other.gameObject.name == "Fortrees" || other.gameObject.name == "Fortrees(Clone)") {
            HP += 5;
            
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.name == "Fortrees" || other.gameObject.name == "Fortrees(Clone)") {
            HP -= 5;
        }
    }
}