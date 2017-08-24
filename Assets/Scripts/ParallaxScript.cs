using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

	//public Rigidbody2D background;
	public float scrollFactor;
	public Rigidbody2D player;
	private float playerXSpeed;
	private float playerYSpeed;
	private float thisSpeedX;
//	private float thisSpeedY;
	public Camera cam;


	// Use this for initialization
	void Start () {
		//rotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		playerXSpeed = player.velocity.x;
		playerYSpeed = player.velocity.y;
		thisSpeedX = playerXSpeed * scrollFactor;
		//thisSpeedY = playerYSpeed * scrollFactor;
		if (scrollFactor != 1) {
			if (cam.GetComponent<CameraBind> ().moving) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (thisSpeedX, 0);
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			}
		} else {//changed position to localPosition
			transform.position = new Vector2 (player.transform.position.x, transform.position.y);
			//transform.rotation = rotation;
		}

	}
}
