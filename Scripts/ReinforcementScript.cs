using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcementScript : MonoBehaviour
{
    public float timer;
    int intTimer;
    public Text timerText;
    public GameObject[] reinforcement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0) {
            timer -= Time.deltaTime;
            intTimer = Convert.ToInt32(timer);
            timerText.text = "Reinforcement come in " + intTimer.ToString();
        }
        else if(timer < 0) {
            timerText.text = "reinforcements have arrived and are on the edge of the map. Now Kill All Enemyes";
            timer = 0;
            for(int i = 0; i < reinforcement.Length; i++) {
                reinforcement[i].SetActive(true);
            }
        }
    }
}
