using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SoldierScript : MonoBehaviour
{
    public float HP;
    private float startHP;
    public GameObject bullet;
    public Transform bullet1;
    public Transform bullet2;
    public LineRenderer laser;
    public Image healthBar;
    public int damage;
    public Transform Weapon;
    [Header("Timer")]
    public float time;
    public float newTime;
    Camera mainCamera;
    NavMeshAgent navMesh;
    BulletScript bulletScript;
    [Header("Targets")]
    public Transform targets;
    public float range;
    [Header("XP")]
    public int xp;
    public int level;
    [Header("Distance")]
    public int distance;
    BulletScript bul1;
    private Animator anim;
    Vector3 oldPos;
    private bool fortres;
    private int a;
    private int b;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        navMesh = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        fortres = false;
        oldPos = this.transform.position;
        // gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
        a = 10;
        b = 20;
        startHP = HP;
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

    // Update is called once per frame
    void Update()
    {

        if(HP <= 0) {
            Destroy(gameObject);
            UnitSelections.instance.Deselect(this.gameObject);
            UnitSelections.instance.unitSelected.Remove(this.gameObject);
            UnitSelections.instance.unitList.Remove(this.gameObject);
        }
        else {
            healthBar.fillAmount = HP / startHP;
        }
        
        if(oldPos != transform.position && targets == null) {
            anim.Play("Walk");
            oldPos = transform.position;
        }
        else if(oldPos == transform.position && targets == null) anim.Play("Idle");

        if(targets == null) {
            if(laser.enabled) laser.enabled = false;
            return;
        }

        if(xp >= a && xp <= b) {
            a *= 2;
            b *= 2;
            level++;
            if(distance != 25 && distance < 25) {
                distance = distance + level;
            }
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


            if(targets.GetComponent<EnemySoldierScript>().HP <= 0) {
                xp++;
            }
        }
    }

    public void Fire() {
        if(bullet != null) {
            Instantiate(bullet, bullet1.position, bullet1.rotation);
            if(bullet2 != null) Instantiate(bullet, bullet2.position, bullet2.rotation);
        }
        else if(laser != null && targets != null) {
            if(!laser.enabled) laser.enabled = true;
            laser.SetPosition(0, bullet1.position);
            laser.SetPosition(1, targets.position);
            if(targets != null) targets.GetComponent<EnemySoldierScript>().HP -= damage;
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