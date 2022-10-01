using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreScript : MonoBehaviour
{
    public int gettingMoney;
    GameObject moneyManager;
    // Start is called before the first frame update
    void Start()
    {
        moneyManager = GameObject.FindGameObjectWithTag("MoneyManager");
        InvokeRepeating("GetOre", 0.0F, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Bronza") {
            gettingMoney = 2;
        }
        if(other.gameObject.name == "Iron") {
            gettingMoney = 5;
        }
        if(other.gameObject.name == "Gold") {
            gettingMoney = 12;
        }
    }

    public void GetOre() {
        moneyManager.GetComponent<MoneyManagerScript>().Money += gettingMoney;
    }
}
