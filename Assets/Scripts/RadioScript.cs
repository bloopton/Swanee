using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioScript : MonoBehaviour {

	public GameObject musicObject;
	public AudioSource backgroundMusic;
	bool playerInTriggerZone;
	Animator animator;
	// Use this for initialization
	void Start () {
		backgroundMusic = musicObject.GetComponent<AudioSource> ();
		playerInTriggerZone = false;
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Volume: " + backgroundMusic.volume);
		if (Input.GetKeyDown (KeyCode.F)) {
			if (playerInTriggerZone) {
					//Debug.Log ("Got F");
				if (backgroundMusic.volume == 1f) {
					backgroundMusic.volume = .05f;
					animator.SetBool ("On", false);
				} else {
					backgroundMusic.volume = 1f;
					animator.SetBool ("On", true);

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
