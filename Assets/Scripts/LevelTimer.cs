using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour {
	public float timeLeft;
	// Use this for initialization
	void Start () {
		float timeLeft = 5f;
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;
		if ( timeLeft < 0 )
		{
			gameObject.GetComponent<ChangeLevelScript>().changeLevel(3);
		}
	}
}
