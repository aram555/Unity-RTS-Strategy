using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrainingScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isClicek;
    public GameObject MoneyMangar;
    public GameObject noMoney;

    private MoneyManagerScript money;

    private void Start() {
        money = MoneyMangar.GetComponent<MoneyManagerScript>();
    }
    public void Training(GameObject unit) {
        GameObject barracks = UnitSelections.instance.unitSelected[0];
        if(unit.gameObject.name == "PlayerSoldier") {
            if(money.Money <= 20) {
                noMoney.SetActive(true);
                return;
            }
            barracks.GetComponent<BarracksScript>().soldierCount++;
            barracks.GetComponent<BarracksScript>().trainingSoldiers = true;
            money.Money -= 20;
        }
        else if(unit.gameObject.name == "PlayerAntiTank") {
            if(money.Money <= 40) {
                noMoney.SetActive(true);
                return;
            }
            barracks.GetComponent<BarracksScript>().RPGCount++;
            barracks.GetComponent<BarracksScript>().trainingRPG = true;
            money.Money -= 40;
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        isClicek = true;
        Debug.Log("Нажата");
    }
    
    public void OnPointerUp(PointerEventData eventData){
        isClicek = false;
        Debug.Log("Не нажато");
    }

}
