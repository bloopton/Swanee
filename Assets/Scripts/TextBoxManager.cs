using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	//public GameObject textBox;
	public Text theText;	
	public TextAsset textFile;
	public Button nextButton;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public PlayerController player;


	public bool isActive;
	public bool stopPlayerMovement;

	private bool isTyping = false;
	private bool cancelTyping = false;

	public float typeSpeed;


	// Use this for initialization
	void Start () {
		if (isActive) {
			EnableTextBox ();
		} else {
			DisableTextBox ();
		}

		player = FindObjectOfType<PlayerController> (); 
		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;
		}
	}

	/*
	void OnEnable(){
		//		Debug.Log("Button Enabled");
		nextButton.onClick.AddListener (Iterate);//enable "next line of text" button
	}
*/
	// Update is called once per frame
	void FixedUpdate () {
		//theText.text = textLines [currentLine];
		//Debug.Log (textLines [currentLine]);

		//if (currentLine >= endAtLine) {
		//	DisableTextBox ();
		//}
		if (Input.GetKeyDown(KeyCode.E)) {
			if (isActive) {//to prevent premature iteration
				Iterate ();
			}
		}
	}

	void Iterate(){
		//	Debug.Log("Button Listener");
		if(!isTyping){
			currentLine += 1;//move to next line of text
			if (currentLine >= endAtLine) {
				DisableTextBox ();
			} else {
				StartCoroutine (TextScroll (textLines [currentLine]));
			}
		} else if(isTyping && !cancelTyping){
			cancelTyping = true;
		}
	}

	private IEnumerator TextScroll(string lineOfText){
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) {
			theText.text += lineOfText [letter];
			letter++;
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;

	}


	public void EnableTextBox(){
	//	textBox.SetActive (true);
		nextButton.gameObject.SetActive(true);
		theText.gameObject.SetActive (true);
		nextButton.enabled = true;
		isActive = true;
		if (stopPlayerMovement) {
			player.canMove = false;
		}
		StartCoroutine (TextScroll (textLines [currentLine]));

	}

	public void DisableTextBox(){
		isActive = false;
		//textBox.SetActive (false);
		theText.gameObject.SetActive (false);
		nextButton.gameObject.SetActive(false);
		nextButton.enabled = false;
		player.canMove = true;
	}

	public void ReloadScript(TextAsset theText){
		if (theText != null) {
			textLines = new string[2];
			textLines = (theText.text.Split ('\n'));
		}
	}
}
