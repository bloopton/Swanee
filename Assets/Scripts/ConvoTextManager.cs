using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConvoTextManager : MonoBehaviour {

	public bool hasLetterSounds;//if true, every letter rendered produces a sound
	public AudioSource letterSound;
	public bool useButton;

	//public GameObject textBox;
	public Text theText;	
	public TextAsset textFile;
	public Button nextButton;
	public string[] textLines;

	public Image convoBackground; //background image of conversation
	public Image talker1;
	public Image talker2;
	string talker1Name;//includes a colon
	string talker2Name;//includes a colon
	string currentTalker;
	string currentHeader;


	public int currentLine;
	public int endAtLine;
	public int initLine;

	public PlayerController player;


	public bool isActive;
	public bool stopPlayerMovement;

	private bool isTyping = false;
	//private bool cancelTyping = false;

	public float typeSpeed;


	// Use this for initialization
	void Start () {

		//allow for multiple convos in one file
		initLine = currentLine;
		//color red

		currentTalker = "";

		if (hasLetterSounds) {
			letterSound = talker1.GetComponent<AudioSource> ();
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


	// Update is called once per frame
	void FixedUpdate () {
		Debug.Log ("Talker 1: " + talker1Name);
		Debug.Log ("Talker 2: " + talker2Name);
		Debug.Log ("Current talker: " + currentTalker);
		//change text sounds, animations depending on current speaker
		//because talker1Name and talker2Name include colons at end
		if (currentTalker+':' == talker1Name) {
			//letterSound = talker1.GetComponent<AudioSource> ();

			talker2.GetComponent<Animator> ().SetBool ("Talking", false);
			talker1.GetComponent<Animator> ().SetBool ("Talking", true);


			//talker2.stop;
			//talker1.animate;
		} else if (currentTalker+':' == talker2Name) {
			//letterSound = talker2.GetComponent<AudioSource> ();

			talker2.GetComponent<Animator> ().SetBool ("Talking", true);
			talker1.GetComponent<Animator> ().SetBool ("Talking", false);

			//talker1.stop;
			//talker2.animate;
		}

		//no talking if not typing
		if (!isTyping) {
			talker1.GetComponent<Animator> ().SetBool ("Talking", false);
			talker2.GetComponent<Animator> ().SetBool ("Talking", false);
		}


		if (Input.GetKeyDown (KeyCode.E)) {
			if (isActive) {//to prevent premature iteration
				Iterate ();
			}
		}
	}
		

	void Iterate(){
		if(!isTyping){//if not typing
			//Debug.Log("Iterate");

			currentLine += 1;//move to next line of text
			if (currentLine >= endAtLine) {//if currentLine surpasses stringLine array size
				DisableTextBox ();//text box is disabled
			} else {//currentLine is within text array boundaries
				StartCoroutine (TextScroll (textLines [currentLine]));//scroll text
			}
		}/* else if(isTyping && !cancelTyping){
			//cancelTyping = true;
			//FOR CONVOS, NO CANCELLING TYPING
		//}*/
	}

	//registers if line contains command or text
	//if command, executes command (talker initialzation or end convo)
	//if text, registers current speaker
	string talkerToggle(string lineOfText){

		//Debug.Log ("Line of text: " + lineOfText);
		int letter = 0;

		bool isConvoLine = false;//any line of convo, not command, beginning with \>
		bool isCommand = false; //command = one of two first lines, or "end", begins with \~
		//bool end = false;

		string temp = "";

		while ((letter < lineOfText.Length)) {
			
			if (lineOfText [letter] == '~') {// ~ command
				isCommand = true;
				//Debug.Log ("Command");
			}
			else if (lineOfText [letter] == '>') {//just conversation line
				//Debug.Log ("Begin convo line");
				isConvoLine = true;
			}
			else if (isConvoLine) {
				//Debug.Log ("Letter is " + letter);
				//Debug.Log ("Bounds are " + lineOfText.Length);
				if (lineOfText [letter] == ':') {
					currentHeader = '>' + temp + ':';
					currentTalker = temp;
					//toggle voices
					if (currentTalker+':' == talker1Name) {
						letterSound = talker1.GetComponent<AudioSource> ();

					} else if (currentTalker+':' == talker2Name) {
						letterSound = talker2.GetComponent<AudioSource> ();
					}
					//iterate through rest of line and return that
					string convoText = "";
					//Debug.Log ("Current talker" + currentTalker);
					//Debug.Log ("Header length" + currentHeader.Length);
					int convoIterator = currentHeader.Length;
					while (convoIterator < lineOfText.Length) {
						convoText += lineOfText [convoIterator];
						convoIterator++;

					}
				//	Debug.Log ("Convo text" + convoText);
					return currentTalker + ": \n" + convoText;

				} else {
					temp += lineOfText [letter]; //get speaking of current line
				}
			}
			else if (isCommand == true) {//if start lines or end line commands, record subsequent letters 
				temp += lineOfText [letter];
			}
				
			letter++;//move to next letter
		}

		if (temp == "end") {
			currentTalker = "";
			isConvoLine = false;
			isCommand = false;
			DisableTextBox ();
		} else {//either Talker 1 or Talker 2 name
			if (currentLine == initLine) {//talker1
				talker1Name = temp;
			} else if (currentLine == initLine + 1) {//talker2
				talker2Name = temp;
			}
		}
		return "";//if not in convoText line, no need to return anything to read/print

	}

	private IEnumerator TextScroll(string lineOfText){

		string convoLine = talkerToggle (lineOfText);//convoLine is line of text w/o \~ && \name && \:
		//Debug.Log("Convo: " + convoLine);
		if (convoLine == "") {
			//Debug.Log("Orig: " + lineOfText);
			if (isActive) {
				//Iterate ();
				while (talkerToggle (lineOfText) == "") {
					currentLine += 1;//move to next line of text
					lineOfText = textLines[currentLine];//reload current text to parse
					//StartCoroutine (TextScroll (textLines [currentLine++]));//scroll text
				}
				convoLine = talkerToggle (lineOfText);
			}
			//yield return null;
		}
		int letter = 0;
		theText.text = "";//text component in text box

		isTyping = true;//currently typing
		//cancelTyping = false;//typing has not been canceled
	
		while ((letter < convoLine.Length)) {
			if (convoLine [letter] == '[') {
				while (convoLine [letter] != ']') {
					letter++;
					if (convoLine [letter] == ']') {
						letter++;
						break;
					}
					theText.text += "<color=red>";
					theText.text += convoLine [letter];//append the current letter of line to text component
					theText.text += "</color>";
					if (hasLetterSounds) {
						letterSound.Play ();//player letter sound if necessary
					}
					yield return new WaitForSeconds (typeSpeed);

				}
			}

			Debug.Log ("Typing: " + convoLine + " letter " + letter);
			theText.text += convoLine [letter];//append the current letter of line to text component
			//Debug.Log ("The text is: " + theText.text);

			letter++;//move to next letter
			if (hasLetterSounds) {
				letterSound.Play ();//player letter sound if necessary
			}
			yield return new WaitForSeconds (typeSpeed);
		}
		//theText.text = convoLine;//currentTalker + " " + convoLine;//defaults to talker name, space, convo
		isTyping = false;
		//cancelTyping = false;

	}


	public void EnableTextBox(){
		//	textBox.SetActive (true);
		if (useButton) {
			nextButton.gameObject.SetActive (true);
			nextButton.enabled = true;
		}
		theText.gameObject.SetActive (true);
		isActive = true;
		if (stopPlayerMovement) {
			player.canMove = false;//by default, player cannot move during conversations
		}
		convoBackground.enabled = true;
		talker1.enabled = true;
		talker2.enabled = true;

		StartCoroutine (TextScroll (textLines [currentLine]));

	}

	public void DisableTextBox(){
		player.canMove = true;

		isActive = false;
		//textBox.SetActive (false);
		theText.gameObject.SetActive (false);
		nextButton.gameObject.SetActive(false);
		nextButton.enabled = false;
		convoBackground.enabled = false;
		talker1.enabled = false;
		talker2.enabled = false;
	}

	public void ReloadScript(TextAsset theText){
		if (theText != null) {
			//textLines = new string[2];
			textLines = (theText.text.Split ('\n'));
		}
	}
}
