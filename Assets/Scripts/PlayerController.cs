using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
	public GameObject manager;
	public bool isActive;
	public KeyCode use, moveRight, moveLeft;
	Rigidbody2D playerRB;
	private Animator animator;
	MovementScript ms;
	public bool canMove;
	// Use this for initialization
	void Start () {
		
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			isActive = false;
		} else {
			isActive = true;
		}
		playerRB = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator> ();
		ms = gameObject.GetComponent<MovementScript>();

		//inspect = KeyCode.E; INSPECT
		//use = KeyCode.F
		moveRight =  KeyCode.D;
		moveLeft = KeyCode.A;
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive == true && manager.GetComponent<ManagerScript> ().hasBegun == false) {
			manager.GetComponent<ManagerScript> ().hasBegun = true;
		}
	}

	void FixedUpdate () {
		if (!canMove) {
			return;
		}
		if (Input.GetKey (moveRight)) {
			if (animator.GetBool ("HasBegun") == false) {
				animator.SetBool ("HasBegun", true);
			}
				
			if (isActive) {//Wait until done getting up from table before allowing movement
				if (playerRB.velocity.y == 0)
					animator.SetBool ("Walking", true);
				ms.moveRight ();
			}
		} else if (Input.GetKey (moveLeft)) {
			if (animator.GetBool ("HasBegun") == false) {
				animator.SetBool ("HasBegun", true);
			}
			if (isActive) {
				if (playerRB.velocity.y == 0)
					animator.SetBool ("Walking", true);
				ms.moveLeft ();
			}
		} else
			animator.SetBool ("Walking", false);
		Debug.Log ("Is active " + isActive);
	}

	public void setActive(){
		isActive = true;
	}
}
