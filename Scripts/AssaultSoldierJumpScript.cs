using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AssaultSoldierJumpScript : MonoBehaviour
{
    private NavMeshAgent navMesh;
    private Rigidbody rb;
    public float speed;
    public float jumpSpeed;
    public float rangeOfJump;
    public bool isGrounded;
    public bool isJuming;
    public LayerMask ground;
    RaycastHit hit;
    Vector3 rayPoint;
    SphereCollider col;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        isGrounded = true;
        isJuming = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKey(KeyCode.J) && isGrounded) {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, ground)) {
                rayPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                Jump();
                
            }
        }

        if(isJuming) {
            transform.position = Vector3.MoveTowards(this.transform.position, rayPoint, speed * Time.deltaTime);
            Vector3 direction = rayPoint - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Quaternion lookAtRotationY = Quaternion.Euler(transform.rotation.eulerAngles.x, lookRotation.eulerAngles.y, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookAtRotationY, 10 * Time.fixedDeltaTime);
        }
    }

    void Jump() {
        navMesh.enabled = false;
        col.isTrigger = false;
        isGrounded = false;
        isJuming = true;
        rb.useGravity = true;
        rb.AddForce(rb.transform.up * 10, ForceMode.Impulse); 
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "Ground") {
            col.isTrigger = true;
            navMesh.enabled = true;
            isGrounded = true;
            isJuming = false;
        }
    }
}
