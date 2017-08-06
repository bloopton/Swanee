using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevelScript : MonoBehaviour {
	bool isLoading;
	int bi;
	// Use this for initialization
	void Start () {
		isLoading = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isLoading) {
			if (!GameObject.FindGameObjectWithTag ("Fade").GetComponent<FadeScript> ().fading) {
				SceneManager.LoadScene (bi);
			}
		}
	}

	public void changeLevel(int buildIndex){
		if (GameObject.FindGameObjectWithTag ("Fade")) {
			GameObject.FindGameObjectWithTag ("Fade").GetComponent<FadeBlackScript> ().fadeToBlack ();
		}
		bi = buildIndex;
		isLoading = true;
	}
}
