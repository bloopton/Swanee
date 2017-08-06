using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFadeHandler : MonoBehaviour {
	public GameObject manager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//test if have begun; if so, fade
		if (manager.GetComponent<ManagerScript> ().hasBegun == true) {
			gameObject.GetComponent<FadeScript> ().StartCoroutine ("FadeOut");
		}
	}
}
