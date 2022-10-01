using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    public Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        oldPos = this.transform.position;
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

		if(gameObject.name == "PlayerSoldier" || gameObject.name == "PlayerSoldier(Clone)") {
			if(oldPos != transform.position) {
				if(gameObject.GetComponent<SoldierScript>().targets != null) {
					anim.Play("Attack");
				}
				else {
					anim.Play("Walk");
				}
				oldPos = transform.position;
			}
			else if(gameObject.GetComponent<SoldierScript>().targets != null) {
				anim.Play("Attack");
			}
			else {
				anim.Play("Idle");
			}
		}

		if(gameObject.name == "PlayerAssaultSoldier" || gameObject.name == "PlayerAssaultSoldier(Clone)") {
			if(oldPos != transform.position) {
				if(gameObject.GetComponent<AssaultSoldierScript>().targets != null) {
					anim.Play("Attack");
				}
				else {
					anim.Play("Walk");
				}
				oldPos = transform.position;
			}
			else if(gameObject.GetComponent<AssaultSoldierScript>().targets != null) {
				anim.Play("Attack");
			}
			else {
				anim.Play("Idle");
			}
		}

        // if(oldPos != transform.position) {
		// 	if(gameObject.name == "PlayerSoldier" || gameObject.name == "PlayerSoldier(Clone)") {
		// 		if(gameObject.GetComponent<SoldierScript>().targets != null) {
		// 			anim.Play("Attack");
		// 		}
		// 		else {
		// 			anim.Play("Walk");
		// 		}
		// 	}
		// 	oldPos = transform.position;
		// }
		// else if(this.gameObject.GetComponent<SoldierScript>().targets != null) {
		// 	if(this.gameObject.name == "PlayerSoldier" || this.gameObject.name == "PlayerSoldier(Clone)") {
        //         anim.Play("Attack");
        //     }
		// }
		// else {
		// 	if(this.gameObject.name == "PlayerSoldier" || this.gameObject.name == "PlayerSoldier(Clone)") {
		// 		anim.Play("Idle");
		// 	}
		// }
    }
}
