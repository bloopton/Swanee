using UnityEngine;
using System.Collections;

public class CameraBind : MonoBehaviour {


	public bool moving;
	Transform target;
	GameObject targetObj;
	public Transform startPos;
	public Transform endPos;
	public float initDistance; //initial distance between player and start wall.
	Camera cam;

	void Awake(){
		cam = GetComponent<Camera> ();
		Vector3 initCamPos = new Vector3 (GameObject.FindGameObjectWithTag ("Player").transform.position.x, cam.transform.position.y, cam.transform.position.z);
		cam.transform.SetPositionAndRotation(initCamPos, cam.transform.rotation);
	}
			
	// Use this for initialization
	void Start () {


		targetObj = GameObject.FindGameObjectWithTag ("Player");
		target = targetObj.transform;

		initDistance = Mathf.Abs(targetObj.transform.position.x - startPos.position.x);
		//distance between camera and start position intially
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if (cam.WorldToViewportPoint (startPos.position).x > 1 && cam.WorldToViewportPoint (startPos.position).x < 0) {
		//	if (cam.WorldToViewportPoint (endPos.position).x > 1 && cam.WorldToViewportPoint (endPos.position).x < 0) {
				
		if (Mathf.Abs (target.position.x - startPos.position.x) > initDistance && Mathf.Abs (target.position.x - endPos.position.x) > initDistance) {//if player moves away from intial position, track
			if (target.transform.position.x > startPos.position.x && target.transform.position.x < endPos.position.x) {
				transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
				moving = true;
			}
		} else {
			moving = false;
		}
		//	}
		//}


		//If not in end condition, possibly track 
		/*
		if (Mathf.Abs (target.position.x - endPos.position.x) > trackDistance) {
			//if start condition, wait until player is at position x = 0 to track
			if (target.position.x >= 0) {
				if (isCamera) {
					transform.position = new Vector3 (target.position.x, transform.position.y, transform.position.z);
				}
			}
		}
		*/
	}
}
