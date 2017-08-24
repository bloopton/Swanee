using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeBlackScript : MonoBehaviour {
	public bool fadeIn;

	// Use this for initialization
	void Start () {
		if (fadeIn) {
			fadeFromBlack ();
		}
	}

	public void fadeFromBlack(){
		gameObject.GetComponent<FadeScript> ().StartCoroutine ("FadeOut");
	}

	public void fadeToBlack(){
		gameObject.GetComponent<FadeScript> ().StartCoroutine ("FadeIn");
	}
	// Update is called once per frame
	void Update () {
		
	}
}
