using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrintSkript : MonoBehaviour
{
    private bool isTrue;
    private GameObject moneyMan;
    private MoneyManagerScript moneyScript;
    private void Start() {
        isTrue = false;
    }
    public void GetMoneyManager(GameObject moneyManager) {
        moneyMan = moneyManager;
        moneyScript = moneyMan.GetComponent<MoneyManagerScript>();
    }
    public void SetPrice(int price) {
        if(moneyScript.Money >= price) {
            moneyScript.Money -= price;
            isTrue = true;
        }
        else if (moneyScript.Money < price) {
            isTrue = false;
            return;
        }
    }
    public void SpawnBluePrintBuilding(GameObject bluePrintBuilding) {
        if(isTrue == true) {
            Instantiate(bluePrintBuilding, transform.position, transform.rotation);
        }
    }
}
