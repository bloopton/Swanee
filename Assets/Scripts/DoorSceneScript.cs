using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSceneScript : MonoBehaviour {

	bool playerInTriggerZone;
	public int nextLevel;
	ChangeLevelScript cls;

	// Use this for initialization
	void Start () {
		playerInTriggerZone = false;
		cls = GetComponent<ChangeLevelScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			if (playerInTriggerZone) {
				if (GetComponent<InteractableScript> ().canUse) {
					cls.changeLevel (nextLevel);
				}
			}
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
