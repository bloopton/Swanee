	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour {
	public GameObject manager;
	public bool isActive;
	public KeyCode use, moveRight, moveLeft, moveDown, moveUp, toggleRun;
	Rigidbody2D playerRB;
	private Animator animator;
	MovementScript ms;
	public bool canMove;
	public bool running, climbing, nearTopLadder, nearBottomLadder;
	float initAnimSpeed;

	// Use this for initialization
	void Start () {
		//default set climbing to false, usually will spawn in standing
		climbing = false;
		if (SceneManager.GetActiveScene ().buildIndex == 0) {
			isActive = false;
		} else {
			isActive = true;
		}
		running = false;
		playerRB = gameObject.GetComponent<Rigidbody2D>();
		animator = gameObject.GetComponent<Animator> ();
		ms = gameObject.GetComponent<MovementScript>();

		initAnimSpeed = animator.speed;


		//inspect = KeyCode.E; INSPECT
		//use = KeyCode.F
		moveRight =  KeyCode.D;
		moveLeft = KeyCode.A;
		moveDown = KeyCode.S;
		moveUp = KeyCode.W;
		toggleRun = KeyCode.LeftShift;

	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log ("Can move" + canMove);

		if (isActive == true && manager.GetComponent<ManagerScript> ().hasBegun == false) {
			manager.GetComponent<ManagerScript> ().hasBegun = true;
		}

		if (climbing) {
			if (animator.GetBool ("Climbing") == false) {
				animator.SetBool ("Climbing", true);
			}
		} else if (!climbing) {
			if (animator.GetBool ("Climbing") == true) {
				animator.SetBool ("Climbing", false);
			}
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
					playerRB.isKinematic = true;
					animator.SetBool ("Climbing", true);

				}
			} 
			else {
				//get off ladder at top
				if (Input.GetKey (moveUp)) {
					climbing = false;
					playerRB.isKinematic = false;
					animator.SetBool ("Climbing", false);


				}
			}
		}
		else if(nearBottomLadder)
		{
			if (!climbing) {
				if (Input.GetKey (moveUp)) {
					climbing = true;
					playerRB.isKinematic = true;
					animator.SetBool ("Climbing", true);

				}
			} 
			else {
				//get off ladder at bottom
				if (Input.GetKey (moveDown)) {
					climbing = false;
					playerRB.isKinematic = false;
					animator.SetBool ("Climbing", false);


				}
			}
		}
			
		//end enter/exit ladder code

		//climb ladder code
		if(climbing){
			
			if (Input.GetKey (moveUp)) {
				ms.moveUp ();
				animator.speed = initAnimSpeed;
			} else if (Input.GetKey (moveDown)) {
				ms.moveDown ();
				animator.speed = initAnimSpeed;

			} else {
				animator.speed = 0;//stop animation if u arent climbing
				playerRB.velocity = new Vector3 (0, 0, 0);//dont want to move up and down if no keys down
			}
		} else if (!climbing){
		//hold shift to prepare for run
		/*	if (Input.GetKey (toggleRun)) {
				running = true;
			} else {
				running = false;
			}
			*/
			if (Input.GetKey (moveRight) && Input.GetKey (toggleRun)) {
				if (animator.GetBool ("HasBegun") == false) {
					animator.SetBool ("HasBegun", true);
				}
				if (isActive) {
					animator.SetBool ("Running", true);
					animator.SetBool ("Walking", false);

					ms.runRight ();
				}
			} else if (Input.GetKey (moveLeft) && Input.GetKey (toggleRun)) {
				if (animator.GetBool ("HasBegun") == false) {
					animator.SetBool ("HasBegun", true);
				}
				if (isActive) {
					animator.SetBool ("Running", true);
					animator.SetBool ("Walking", false);

					ms.runLeft ();
				}
			} else if (Input.GetKey (moveRight)) {
				if (animator.GetBool ("HasBegun") == false) {
					animator.SetBool ("HasBegun", true);
				}
				
				if (isActive) {//Wait until done getting up from table before allowing movement
					if (playerRB.velocity.y == 0)
				
						ms.moveRight ();
					animator.SetBool ("Walking", true);
					animator.SetBool ("Running", false);



					//	}
				}
			} else if (Input.GetKey (moveLeft)) {
				if (animator.GetBool ("HasBegun") == false) {
					animator.SetBool ("HasBegun", true);
				}
				if (isActive) {
					if (playerRB.velocity.y == 0)
				
						ms.moveLeft ();
					animator.SetBool ("Walking", true);
					animator.SetBool ("Running", false);


					//}
				}
			} else {
				animator.SetBool ("Walking", false);
				animator.SetBool ("Running", false);

			}

		}
	}

	public void setActive(){
		isActive = true;
	}
}
