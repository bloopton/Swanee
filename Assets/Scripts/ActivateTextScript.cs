using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextScript : MonoBehaviour {

	public TextAsset theText;
	public int startLine;
	public int endLine;
	public TextBoxManager theTextBox;
	public bool requireActivate;//need to press e to show text
	private bool waitForPress;
	private bool pressed;
	public PlayerController player;


	public bool destroyWhenActivated;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
		if(theTextBox == null) 
			theTextBox = FindObjectOfType<TextBoxManager> ();
		//theText = theTextBox.textFile;
		theTextBox.textFile = theText;

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)){
			if(requireActivate){
				if (pressed == false) {
					Use ();
				}

			}
		}
		if (theTextBox.currentLine >= theTextBox.endAtLine) {
			pressed = false;//allow for reactivation if text is gone
		}
	}



	void Use(){
		if (waitForPress) {
			if (pressed == false) {
				pressed = true;
				//Debug.Log ("Click!");
				theTextBox.ReloadScript (theText);
				theTextBox.currentLine = startLine;
				theTextBox.endAtLine = endLine;
				theTextBox.EnableTextBox ();
				if (destroyWhenActivated) {
					Destroy (gameObject);
				}
			}
		}

	}

	void OnTriggerEnter2D(Collider2D other){
			if (other.name == "Player") {
				if (requireActivate) {
				waitForPress = true;
					return;
				}

			theTextBox.ReloadScript (theText);
			theTextBox.currentLine = startLine;
			theTextBox.endAtLine = endLine;
			theTextBox.EnableTextBox();
			if (destroyWhenActivated) {
				Destroy (gameObject);
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.name == "Player") {
			pressed = false;
			waitForPress = false;
		}
	}
}
