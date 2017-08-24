using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCollisionsScript : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void EnableCols(){
		GetComponent<BoxCollider2D> ().enabled = true;
	}
	void DisableCols(){
		GetComponent<BoxCollider2D> ().enabled = false;

	}

}
