using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour {

	public bool top;
	public bool bottom;
	//public GameObject player;

	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			PlayerController pc = col.gameObject.GetComponent <PlayerController>();
			if (top) {
				pc.nearTopLadder = true;
			} else if (bottom) {
				pc.nearBottomLadder = true;
			}
		}
	}
	void OnTriggerExit2D(Collider2D col){
		if (col.gameObject.tag == "Player") {
			PlayerController pc = col.gameObject.GetComponent <PlayerController>();
			if (top) {
				pc.nearTopLadder = false;
			} else if (bottom) {
				pc.nearBottomLadder = false;
			}
		}
	}
}
