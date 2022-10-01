using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemySoldierScript : MonoBehaviour
{
    public float HP;
    float startHP;
    public GameObject bullet;
    public Transform bullet1;
    public LineRenderer laser;
    public Transform targets;
    public Transform buildings;
    public float range;
    public float BuildingRangerange;
    public int distance;
    public Image healthBar;
    [Header("Timer")]
    public float time;
    public float newTime;
    public float navMeshTimer;
    Transform point;
    private BulletScript enemyBullet;
    private NavMeshAgent navMesh;
    Animator anim;
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        anim = GetComponent<Animator>();
        oldPos = this.transform.position;
        startHP = HP;
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.SetDestination(MoveToPoint(navMesh, MovePoints.points[Random.Range(0, MovePoints.points.Length - 1)].position, 10));
        point = MovePoints.points[Random.Range(0, MovePoints.points.Length - 1)];
    }
    public void UpdateTarget() {
        GameObject[] Enemyes = GameObject.FindGameObjectsWithTag("Player");
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

    public static Vector3 MoveToPoint(NavMeshAgent agent, Vector3 center, float radius) {
        var randDIrection = Random.insideUnitSphere * radius;
        randDIrection += center;
        NavMeshHit hit;
        NavMesh.SamplePosition(randDIrection, out hit, radius, -1);
        return hit.position;
    }

    // Update is called once per frame
    void Update()
    {
        navMeshTimer -= Time.deltaTime;
        if(navMeshTimer <= 0 && targets == null) {
            Vector3 newPosition = new Vector3(point.position.x, 0, point.position.z);
            navMeshTimer = 5;
            navMesh.SetDestination(MoveToPoint(navMesh, newPosition, 10));
        }

        if(oldPos != transform.position && targets == null) {
            anim.Play("Walk");
            oldPos = transform.position;
        }
        else if(oldPos == transform.position && targets == null) anim.Play("Idle");

        if(HP <= 0) {
            Destroy(gameObject);
        }
        else {
            healthBar.fillAmount = HP / startHP;
        }
        

        if(targets == null) {
            return;
        }
        
        if(Vector3.Distance(transform.position, targets.transform.position) < distance) {
            Vector3 direction = targets.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10 * Time.fixedDeltaTime);
            
            time -= Time.deltaTime;
            if(time <= 0) {
                anim.Play("Attack");
                time = newTime;
            }
        }

        
    }

    public void Fire() {
        if(bullet != null) {
            Instantiate(bullet, bullet1.position, bullet1.rotation);
        }
        else if(laser != null && targets != null) {
            if(!laser.enabled) laser.enabled = true;
            laser.SetPosition(0, bullet1.position);
            laser.SetPosition(1, targets.position);
        }
    }
    public void HideLaser() {
        if(laser == null) return;
        else if(laser != null && laser.enabled == true) laser.enabled = false;
    }
    public void Timer() {
        time = newTime;
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PlayerBullet")) {
            Destroy(other.gameObject);
            Debug.Log("Pow");
            HP -= 2;
        }
        if(other.gameObject.name == "RocketBullet" || other.gameObject.name == "RocketBullet(Clone)") {
            Destroy(other.gameObject);
            Debug.Log("Pow");
            HP -= 5;
        }
    }
}
