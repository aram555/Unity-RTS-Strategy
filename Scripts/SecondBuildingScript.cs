using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBuildingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Ground")) {
            if(other.gameObject.name == "Barracks(Clone)") return;
            else Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other) {
        if(other.collider.tag != "Ground") {
            if(other.gameObject.name == "Barracks(Clone)") return;
            else Destroy(other.gameObject);
        }
    }
}
