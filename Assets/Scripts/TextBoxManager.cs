using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public bool hasLetterSounds;//if true, every letter rendered produces a sound
	public AudioSource letterSound;

	int beforeFadeCounter;

	public bool fadeOut;//if true, rendered full line fades out after specified time
	public float fadeDelay;//time to let text linger

	public bool useButton;

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

		beforeFadeCounter = 0;

		if (hasLetterSounds) {
			letterSound = GetComponent<AudioSource> ();
		}

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
		
	void FixedUpdate () {

		if (!fadeOut) {
			if (Input.GetKeyDown (KeyCode.E)) {
				if (isActive) {//to prevent premature iteration
					if (useButton) {
						Iterate ();
					}
				}
			}
		}
		fadeCleanUp ();
	}

	public void fadeCleanUp(){
		//if faded, disable text box
		if (theText.color.a <= 0f) {
			DisableTextBox ();
		}
	}

	//fading text
	public IEnumerator FadeTextToFullAlpha(float t)
	{
		Text i = theText;

		if (beforeFadeCounter < 1) {
			beforeFadeCounter++;
			yield return new WaitForSeconds (fadeDelay);
		}
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t)
	{
		Text i = theText;

		if (beforeFadeCounter < 1) {
			beforeFadeCounter++;
			yield return new WaitForSeconds (fadeDelay);
		}
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0.0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}

	void Iterate(){
		Debug.Log("Current line " + currentLine);
		Debug.Log("Text " + textLines[currentLine]);
		if(!isTyping){
			currentLine += 1;//move to next line of text
			if (currentLine >= endAtLine) {
				DisableTextBox ();//text box is disabled
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
			if (hasLetterSounds) {
				letterSound.Play ();
			}
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;

		if (fadeOut) {//fade text out
			
			StartCoroutine(FadeTextToZeroAlpha(1f));
			//Debug.Log ("End");
		}

	}


	public void EnableTextBox(){
	//	textBox.SetActive (true);
		//Debug.Log ("Enabing text box");

		if (useButton) {
			nextButton.gameObject.SetActive (true);
			nextButton.enabled = true;
		}
		theText.gameObject.SetActive (true);
		isActive = true;
		if (stopPlayerMovement) {
			player.canMove = false;
		}
		StartCoroutine (TextScroll (textLines [currentLine]));

	}

	public void DisableTextBox(){
		if (stopPlayerMovement) {//so that other stopPM scripts are not sabotaged
			player.canMove = true; 
		}
		isActive = false;
		//textBox.SetActive (false);
		theText.gameObject.SetActive (false);
		if (useButton) {
			nextButton.gameObject.SetActive (false);
			nextButton.enabled = false;
		}
		//Debug.Log ("Disabling text box");
	}

	public void ReloadScript(TextAsset theText){
		if (theText != null) {
			//textLines = new string[];
			textLines = (theText.text.Split ('\n'));
		}
	}
}
