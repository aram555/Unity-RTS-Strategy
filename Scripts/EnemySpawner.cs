using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject[] enemySoldiers;
    public float timer1;
    public float timer2;
    public ReinforcementScript reinforcementScript;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    // Update is called once per frame
    void Update()
    {
        if(reinforcementScript.timer <= 0) {
            StopAllCoroutines();
        }
    }

    private IEnumerator EnemySpawn() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(timer1, timer2));
            int randomEnemy = Random.Range(0, enemySoldiers.Length);
            int rnadomPosition = Random.Range(0, spawners.Length);
            Instantiate(enemySoldiers[randomEnemy], spawners[rnadomPosition].position, Quaternion.identity);
            if(reinforcementScript.timer <= 0) yield break;
        }
    }
}
