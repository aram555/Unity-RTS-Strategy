using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UnitSelections.instance.unitList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy() {
        UnitSelections.instance.Deselect(this.gameObject);
    }
}
