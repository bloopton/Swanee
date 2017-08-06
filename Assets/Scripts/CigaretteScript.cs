using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CigaretteScript : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(player.GetComponent<PlayerController> ().GetComponent<Animator>().GetBool("HasBegun") == true){
			Destroy (gameObject);
		}
	}
}
