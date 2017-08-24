using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLevelDoorScript : MonoBehaviour {

	Animator animator;
	bool playerInTriggerZone;

	// Use this for initialization
	void Start () {
		playerInTriggerZone = false;
		animator = GetComponent<Animator> ();

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			if (playerInTriggerZone) {
				if (GetComponent<InteractableScript> ().canUse) {
					//if (animator.GetBool ("Used") == false) {
					//	Transform playerT = GameObject.FindGameObjectWithTag ("Player").transform;
					//	Transform thisT = GetComponent<SpriteRenderer> ().transform;

					//	thisT.rotation = Quaternion.Euler(thisT.rotation.x, 180, thisT.rotation.z);
					//}
					animator.SetBool ("Used", true);

				}
			}
		} else {
			animator.SetBool ("Used", false);

		}

	}

	void OnTriggerStay2D(Collider2D other){
		//decreasevolume or increase
		if (other.tag == "Player") {
			playerInTriggerZone = true;
		}
	}
	void OnTriggerExit2D(Collider2D other){
		playerInTriggerZone = false;
	}
}
