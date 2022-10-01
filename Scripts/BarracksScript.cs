using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksScript : MonoBehaviour
{
    public float HP;
    float startHP;
    public Image healthBar;
    public float timer;
    public float newTimer;
    public int soldierCount;
    public int RPGCount;
    [Header("Training Bool")]
    public bool trainingSoldiers;
    public bool trainingRPG;
    [Header("Prefabs")]
    public GameObject PlayerSoldier;
    public GameObject PlayerRPG;
    [Header("SpawnPoints")]
    public Transform spawnPos1;
    protected GameObject moneyManager;
    MoneyManagerScript moneyScript;
    // Start is called before the first frame update
    void Start()
    {
        trainingSoldiers = false;
        trainingRPG = false;
        moneyManager = GameObject.FindGameObjectWithTag("MoneyManager");
        moneyScript = moneyManager.GetComponent<MoneyManagerScript>();
        soldierCount = 0;
        RPGCount = 0;
        startHP = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0) Destroy(this.gameObject);
        else healthBar.fillAmount = HP / startHP;

        if(Input.GetKeyDown(KeyCode.T)) {
            if(moneyScript.Money >= 20) {
                moneyScript.Money -= 20;
                soldierCount++;
                if(soldierCount > 0) {
                    trainingSoldiers = true;
                }
            }
            else {
                return;
            }
        }

        if(Input.GetKeyDown(KeyCode.Y)) {
            if(moneyScript.Money >= 40) {
                moneyScript.Money -= 40;
                RPGCount++;
                if(RPGCount > 0) {
                    trainingRPG = true;
                }
            }
            else {
                return;
            }
        }

        if(soldierCount <= 0) {
            trainingSoldiers = false;
        }
        if(RPGCount <= 0) {
            trainingRPG = false;
        }

        if(trainingSoldiers) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                Instantiate(PlayerSoldier, spawnPos1.position, spawnPos1.rotation);
                timer = newTimer;
                soldierCount--;
            }
        }

        if(trainingRPG) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                Instantiate(PlayerRPG, spawnPos1.position, spawnPos1.rotation);
                timer = newTimer;
                RPGCount--;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("EnemyBullet")) {
            Destroy(other.gameObject);
            HP -= 2;
        }
    }
}
