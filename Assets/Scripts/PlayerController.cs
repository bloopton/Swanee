using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
	public GameObject manager;
	public bool isActive;
	public KeyCode use, moveRight, moveLeft, moveDown, moveUp;
	Rigidbody2D playerRB;
	private Animator animator;
	MovementScript ms;
	public bool canMove;
	public bool climbing, nearTopLadder, nearBottomLadder;
	// Use this for initialization
	void Start () {
		//default set climbing to false, usually will spawn in standing
		climbing = false;
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
		moveDown = KeyCode.S;
		moveUp = KeyCode.W;
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
		//enter/exit ladder code
		if (nearTopLadder) {
			if (!climbing) {
				if (Input.GetKey (moveDown)) {
					climbing = true;
				}
			} 
			else {
				//get off ladder at top
				if (Input.GetKey (moveUp)) {
					climbing = false;
				}
			}
		}
		else if(nearBottomLadder)
		{
			if (!climbing) {
				if (Input.GetKey (moveUp)) {
					climbing = true;
				}
			} 
			else {
				//get off ladder at bottom
				if (Input.GetKey (moveDown) || Input.GetKey(moveRight) || Input.GetKey(moveLeft)) {
					climbing = false;
				}
			}
		}
			
		//end enter/exit ladder code

		//climb ladder code
		if(climbing){
			if (Input.GetKey (moveUp)) {
				//MoveScript moveup
			}
			else if (Input.GetKey (moveDown)) {
				//movescript movedown
			}
		} else {
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
		}
	}

	public void setActive(){
		isActive = true;
	}
}
