using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelections : MonoBehaviour
{
    public List<GameObject> unitList = new List<GameObject>();
    public List<GameObject> unitSelected = new List<GameObject>();

    private static UnitSelections _instance;
    public static UnitSelections instance {get {return _instance;} }
    [Header("UI")]
    public GameObject soldierPanel;

    UnitNav unitanim;

    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
    }

    public void ClickSelect(GameObject unitToAdd) {
        DeselectAll();
        unitSelected.Add(unitToAdd);
        //unitToAdd.transform.GetComponent<UnitMovement>().enabled = true;
        unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        
        if(unitToAdd.name == "AssaultSoldierSquad" || unitToAdd.name == "AssaultSoldierSquad(Clone)") {
            unitToAdd.transform.GetComponent<AssaultSoldierJumpScript>().enabled = true;
        }
        if(unitToAdd.name == "Barracks" || unitToAdd.name == "Barracks(Clone)") {
            soldierPanel.SetActive(true);
        }
        
    }

    public void ShiftClickSelect(GameObject unitToAdd) {
        if(!unitSelected.Contains(unitToAdd)) {
            unitSelected.Add(unitToAdd);
            //unitToAdd.transform.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        
        if(unitToAdd.name == "AssaultSoldierSquad" || unitToAdd.name == "AssaultSoldierSquad(Clone)") {
            unitToAdd.transform.GetComponent<AssaultSoldierJumpScript>().enabled = true;
        }
        } else {
            unitSelected.Remove(unitToAdd);
            //unitToAdd.transform.GetComponent<UnitMovement>().enabled = false;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
        
        if(unitToAdd.name == "AssaultSoldierSquad" || unitToAdd.name == "AssaultSoldierSquad(Clone)") {
            unitToAdd.transform.GetComponent<AssaultSoldierJumpScript>().enabled = false;
        }
        }
    }

    public void DragSelect(GameObject unitToAdd) {
        if(!unitSelected.Contains(unitToAdd)) {
            unitSelected.Add(unitToAdd);
            //unitToAdd.transform.GetComponent<UnitMovement>().enabled = true;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(true);
        
        if(unitToAdd.name == "AssaultSoldierSquad" || unitToAdd.name == "AssaultSoldierSquad(Clone)") {
            unitToAdd.transform.GetComponent<AssaultSoldierJumpScript>().enabled = true;
        }
        }
        else {
            unitSelected.Remove(unitToAdd);
            //unitToAdd.transform.GetComponent<UnitMovement>().enabled = false;
            unitToAdd.transform.GetChild(0).gameObject.SetActive(false);
        
        if(unitToAdd.name == "AssaultSoldierSquad" || unitToAdd.name == "AssaultSoldierSquad(Clone)") {
            unitToAdd.transform.GetComponent<AssaultSoldierJumpScript>().enabled = false;
        }
        }
    }

    public void DeselectAll() {
        foreach(var unit in unitSelected) {
            //unit.transform.GetComponent<UnitMovement>().enabled = false;
            unit.transform.GetChild(0).gameObject.SetActive(false);

            if(unit.name == "AssaultSoldierSquad" || unit.name == "AssaultSoldierSquad(Clone)") {
                unit.transform.GetComponent<AssaultSoldierJumpScript>().enabled = false;
            }
            if(unit.name == "Barracks" || unit.name == "Barracks(Clone)") {
            soldierPanel.SetActive(false);
        }
        }
        unitSelected.Clear();
    }

    public void Deselect(GameObject unitToDeselect) {
        unitSelected.Remove(unitToDeselect);
        //unitToDeselect.transform.GetComponent<UnitMovement>().enabled = false;
        unitToDeselect.transform.GetChild(0).gameObject.SetActive(false);
        
        if(unitToDeselect.name == "AssaultSoldierSquad" || unitToDeselect.name == "AssaultSoldierSquad(Clone)") {
            unitToDeselect.transform.GetComponent<AssaultSoldierJumpScript>().enabled = false;
        }
        if(unitToDeselect.name == "Barracks" || unitToDeselect.name == "Barracks(Clone)") {
            soldierPanel.SetActive(false);
        }
    }
}