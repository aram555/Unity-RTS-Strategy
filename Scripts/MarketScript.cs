using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP;
    float startHP;
    public Image healthBar;
    public float timer;
    public float newTimer;
    public int giveMoney;
    GameObject moneyManager;
    MoneyManagerScript moneyScript;
    void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("MoneyManager");
        moneyScript = moneyManager.GetComponent<MoneyManagerScript>();
        startHP = HP;
        MarketsCount.Markets.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0) {
            Destroy(this.gameObject);
            MarketsCount.Markets.Remove(this.gameObject);
        }
        else healthBar.fillAmount = HP / startHP;
        timer -= Time.deltaTime;
        if(timer <= 0) {
            timer = newTimer;
            moneyScript.Money += giveMoney;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("EnemyBullet")) {
            Destroy(other.gameObject);
            HP -= 2;
        }
        if(other.gameObject.name == "EnemyRocketBullet" || other.gameObject.name == "EnemyRocketBullet(Clone)") {
            Destroy(other.gameObject);
            HP -= 10;
        }
    }
}
