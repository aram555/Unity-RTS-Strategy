using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject bullet;
    public int distance;
    public int speed;
    public float minY;
    public float maxY;
    [HideInInspector]
    public int i;
    private Transform target;
    private float damage;
    // Start is called before the first frame update

    public void Seek(Transform _target, float _damage) {
        target = _target;
        damage = _damage;
    }
    private void Start() {
        transform.rotation *= Quaternion.Euler(0, Random.Range(minY, maxY), 0);
    }

    void Update() {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(this.gameObject, distance);
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision other) {
        Destroy(this.gameObject);
    }
}
