using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundScript : MonoBehaviour {

	public AudioSource sound;
	public bool loop;

	// Use this for initialization
	void Start () {
		//test:  sound.Play();

	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<Animator> () != null) {//if animation speed = 0, then do not play
			if (GetComponent<Animator> ().speed == 0) {
				sound.Stop ();
			}
		}
	}

	void playSound(){
		if (!sound.isPlaying) {
			sound.loop = loop;
			sound.Play ();
			
		}
	}

	void stopSound(){
		sound.Stop ();
	}
}
