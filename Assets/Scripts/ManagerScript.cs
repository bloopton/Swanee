using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour {

	//once player tries to move, begin
	public bool hasBegun;
	public GameObject player;
	// Use this for initialization
	void Awake () {
		hasBegun = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
