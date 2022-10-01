using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretScript : MonoBehaviour
{
    public float HP;
    private float startHP;
    public GameObject bullet;
    public Transform bullet1;
    public Transform bullet2;
    public LineRenderer laser;
    public Image healthBar;
    public int damage;
    [Header("Timer")]
    public float time;
    public float newTime;
    BulletScript bulletScript;
    [Header("Targets")]
    public Transform targets;
    public float range;
    [Header("Distance")]
    public int distance;

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
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
            Destroy(this.gameObject);
        }
        else {
            healthBar.fillAmount = HP / startHP;
        }

        if(targets == null) return;

        if(Vector3.Distance(transform.position, targets.transform.position) < distance) {
            Vector3 direction = targets.transform.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 10 * Time.fixedDeltaTime);
            
            
            time -= Time.deltaTime;
            if(time <= 0) {
                Instantiate(bullet, bullet1.position, bullet1.rotation);
                Instantiate(bullet, bullet2.position, bullet2.rotation);
                time = newTime;
            }
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
