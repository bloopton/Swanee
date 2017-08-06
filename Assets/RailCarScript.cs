using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailCarScript : MonoBehaviour {
	MovementScript ms;
	// Use this for initialization
	void Start () {
		ms = gameObject.GetComponent<MovementScript> ();
		InvokeRepeating("bounce", 1.5f, 0.05f);

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Space)) {
			//
		}
	}

	void bounce(){
		ms.jump ();
	}
}
