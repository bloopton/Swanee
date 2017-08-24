using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFadeHandler : MonoBehaviour {
	public GameObject manager;
	public bool triggerFade;//fades on a trigger, like intial movement in first scene

	bool faded;

	// Use this for initialization
	void Start () {
		faded = false;
	}

	// Update is called once per frame
	void Update () {
		//test if have begun; if so, fade
		if (triggerFade && !faded) {
			if (manager.GetComponent<ManagerScript> ().hasBegun == true) {
				gameObject.GetComponent<TextBoxManager> ().StartCoroutine ("FadeTextToZeroAlpha", .5f);
				faded = true;
			}
		}
	}
}
