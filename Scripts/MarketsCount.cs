using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketsCount : MonoBehaviour
{
    public static List<GameObject> Markets = new List<GameObject>();
    public Text MarketsCountText;
    public GameObject defeatText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MarketsCountText.text = "Defend the Markets - " + Markets.Count.ToString() + "/5";

        if(Markets.Count <= 0) {
            defeatText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}