using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    public ReinforcementScript reinforcementScript;
    public GameObject VictoryText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(reinforcementScript.timer <= 0) {
            GameObject[] Enemies = GameObject.FindGameObjectsWithTag("EnemySoldier");

            if(Enemies.Length <= 0) {
                VictoryText.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void Restart() {
        SceneManager.LoadScene(0);
    }
}
