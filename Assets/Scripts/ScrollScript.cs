using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour {
	public bool goingRight;
	public bool dupLoop;//duplicates itself for loop

	public float velocityX;
	public GameObject start;
	public GameObject end;
	// Use this for initialization
	void Start () {
		if (!dupLoop) {
			transform.position = new Vector2 (start.transform.position.x, transform.position.y);
			//GetComponent<Rigidbody2D> ().velocity = new Vector2(velocityX, 0);
		} else {

		}

	}

	void FixedUpdate(){
		transform.localPosition = new Vector2(transform.localPosition.x + velocityX/250f, transform.localPosition.y);
	}

	// Update is called once per frame
	void Update () {

		if (goingRight) {
			if (transform.position.x >= end.transform.position.x) {
				transform.position = new Vector2(start.transform.position.x, transform.position.y);

			}
		}
		else if (transform.position.x <= end.transform.position.x) {
			transform.position = new Vector2(start.transform.position.x, transform.position.y);
		}
	}
}
