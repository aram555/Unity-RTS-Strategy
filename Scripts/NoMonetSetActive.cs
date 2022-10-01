using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMonetSetActive : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) {
            this.gameObject.SetActive(false);
            timer = 5;
        }
    }
}
