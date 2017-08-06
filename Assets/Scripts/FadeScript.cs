using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour {

	public float fadeSpeed = 1f;
	public float fadeTime = 1f;
	public bool fading;
	private SpriteRenderer[] sprites;
	private MeshRenderer mr;


	void Awake(){
		fading = false;
		Debug.Log ("Alpha created");
		if (gameObject.GetComponent<SpriteRenderer> () != null) {
			Debug.Log ("ONE SR");
			sprites = gameObject.GetComponents<SpriteRenderer> ();
			Debug.Log (sprites [0]);
			//Debug.Log (sprites [1]);

		} else {
			sprites =  gameObject.GetComponentsInChildren<SpriteRenderer>();
		}
		foreach (SpriteRenderer s in sprites) {
			Color c = s.color;
			c.a = 1;
			s.color = c;
		}
	}
	// Update is called once per frame
	void Update () {
	}

	IEnumerator FadeIn() {
		fading = true;
		foreach (SpriteRenderer s in sprites) {
			IEnumerator coroutine = FadeInMethod (s);
			StartCoroutine (coroutine);
			yield return null;
		}
	}

	IEnumerator FadeInMethod(SpriteRenderer s){
		for (float f = s.color.a; f <= 1; f += 0.1f) {
			Color c = s.color;
			c.a = f;
			s.color = c;
			if (s.Equals (sprites [sprites.Length-1])) {
				if(f > .9f)
					fading = false;
			}
			yield return new WaitForSeconds (fadeSpeed);
		}
	}

	IEnumerator FadeOut() {
		Debug.Log (sprites [0]);

		foreach (SpriteRenderer s in sprites) {
			//fading = true;
			IEnumerator coroutine = FadeMethod (s);
			StartCoroutine (coroutine);
			yield return null;
		}
		fading = false;

	}
	IEnumerator FadeMethod(SpriteRenderer s){
		for (float f = s.color.a; f >= -1; f -= 0.1f) {
			Color c = s.color;
			c.a = f;
			s.color = c;
			yield return new WaitForSeconds (fadeSpeed);
		}
	}
}