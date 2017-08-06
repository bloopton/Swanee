using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractableScript: MonoBehaviour {

	public bool canUse;
	public bool canInspect;
	Animator animator;

	public Button eIcon;
	public Button fIcon;


	int level = 0;//0 = can click options, 1 = can click use/inspect/bag

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
			fIcon.gameObject.SetActive(false);
			fIcon.enabled = false;

			eIcon.gameObject.SetActive(false);
			eIcon.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		//	Debug.Log ("Num icons " + manager.numOptionIcons);

	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			animator.SetBool ("Activated", true);
			if (canUse) {
				fIcon.gameObject.SetActive (true);
				fIcon.enabled = true;

			}
			if (canInspect) {
				eIcon.gameObject.SetActive (true);
				eIcon.enabled = true;
			}
		}
		//Display "Press F to use" if usable
		//Display "Press E to inspect" if inspectable
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			animator.SetBool ("Activated", false);
			if (canUse) {
				fIcon.gameObject.SetActive (false);
				fIcon.enabled = false;

			}
			if (canInspect) {
				eIcon.gameObject.SetActive (false);
				eIcon.enabled = false;
			}
		}
	}

}
