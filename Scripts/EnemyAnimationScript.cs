using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationScript : MonoBehaviour
{
    private Animator anim;
    private Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldPos != transform.position) {
			if(this.gameObject.name == "EnemySoldier" || this.gameObject.name == "EnemySoldier(Clone)") {
				if(this.gameObject.GetComponent<EnemySoldierScript>().targets != null) {
					anim.Play("Attack");
				}
				else {
					anim.Play("Walk");
				}
			}
			if(this.gameObject.name == "EnemyRPGSoldier" || this.gameObject.name == "EnemyRPGSoldier(Clone)") {
				if(this.gameObject.GetComponent<EnemySoldierScript>().targets != null) {
					anim.Play("RPGAttack");
				}
				else {
					anim.Play("RPGWalk");
				}
			}
			oldPos = transform.position;
		}
		else if(this.gameObject.GetComponent<EnemySoldierScript>().targets != null) {
			if(this.gameObject.name == "EnemySoldier" || this.gameObject.name == "EnemySoldier(Clone)") {
                anim.Play("Attack");
            }
            else if(this.gameObject.name == "EnemyRPGSoldier" || this.gameObject.name == "EnemyRPGSoldier(Clone)") {
                anim.Play("RPGAttack");
            }
		}
		else {
			if(this.gameObject.name == "PlayerSoldier" || this.gameObject.name == "PlayerSoldier(Clone)") {
				anim.Play("Idle");
			}
			else if(this.gameObject.name == "EnemyRPGSoldier" || this.gameObject.name == "EnemyRPGSoldier(Clone)") {
				anim.Play("RPGIdle");
			}
		}
		
    }
}
