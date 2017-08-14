using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	public GameObject manager;
	private ManagerScript managerScript;

	public Vector2 initPos;
	public float jumpPower;
	public float runForce;
	public float runVel;
	public float crawlForce;
	public float crawlVel;
	Rigidbody2D playerRB;
	public int dir;

	// Use this for initialization
	void Start () {
		//managerSingleton = GameObject.FindGameObjectWithTag ("ManagerSingleton");
		managerScript = manager.GetComponent<ManagerScript> ();
		dir = -1;
		playerRB = gameObject.GetComponent<Rigidbody2D>();
		initPos = playerRB.position;
	}

	// Update is called once per frame
	void FixedUpdate () {
			if (gameObject.GetComponent<Rigidbody2D> ().velocity.x > 0.02f) {
				transform.rotation = Quaternion.Euler (0, 0, 0);//face right
				dir = -1;
				//Debug.Log ("Switch to Right");

			} else if (gameObject.GetComponent<Rigidbody2D> ().velocity.x < -0.02f) {
				transform.rotation = Quaternion.Euler (0, 180, 0);//face left
				dir = 1;
				//Debug.Log ("Switch to Left");

			}
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log ("velocity: " + gameObject.GetComponent<Rigidbody2D> ().velocity.x);
	}

	public void moveRight(){
		if (Mathf.Abs (playerRB.velocity.x) < runVel)
			playerRB.AddForce (new Vector2 (runForce, 0), ForceMode2D.Force);
	}
	public void moveLeft(){
		if (Mathf.Abs (playerRB.velocity.x) < runVel)
			playerRB.AddForce (new Vector2 (-runForce, 0), ForceMode2D.Force);
	}
	public void runRight(){
		if (Mathf.Abs (playerRB.velocity.x) < runVel)
			playerRB.AddForce (new Vector2 (runForce*1.5f, 0), ForceMode2D.Force);
	}
	public void runLeft(){
		if (Mathf.Abs (playerRB.velocity.x) < runVel)
			playerRB.AddForce (new Vector2 (-runForce*1.5f, 0), ForceMode2D.Force);
	}

	public void moveUp(){
			playerRB.velocity = new Vector3 (0, 1.5f, 0);
	}

	public void moveDown(){
			playerRB.velocity = new Vector3 (0, -1.5f, 0);
	}
	public void jump(){
		playerRB.AddForce (new Vector2 (0, jumpPower), ForceMode2D.Impulse);
	}



	public void crawlRight(){
		if (Mathf.Abs (playerRB.velocity.x) < crawlVel)
			playerRB.AddForce (new Vector2 (crawlForce, 0), ForceMode2D.Force);
	}

	public void crawlLeft(){
		if (Mathf.Abs (playerRB.velocity.x) < crawlVel)
			playerRB.AddForce (new Vector2 (-crawlForce, 0), ForceMode2D.Force);
	}

	public void crawlUp(){
		if (Mathf.Abs (playerRB.velocity.y) < crawlVel)
			playerRB.AddForce (new Vector2 (0, crawlForce), ForceMode2D.Force);
	}

	public void crawlDown(){
		if (Mathf.Abs (playerRB.velocity.y) < crawlVel)
			playerRB.AddForce (new Vector2 (0, -crawlForce), ForceMode2D.Force);
	}

	public void track(Vector2 pos){
		if (transform.position.x < (pos.x - 0.1f)) {//offset to prevent spaz flipping
			crawlRight();
			//moveRight();
		} else if (transform.position.x > (pos.x + 0.1f)) {
			crawlLeft();
			//moveLeft();
		}

		if (transform.position.y < (pos.y - 0.1f)) {
			crawlUp();
			//moveUp();
		} else if (transform.position.y > (pos.y + 0.1f)) {
			crawlDown();
			//moveDown();
		}

	}

}