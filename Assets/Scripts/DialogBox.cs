using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBox : MonoBehaviour {
	public Text uiText;
	public Image uiImage;
	public Image moreTextIndicator;
	public string[] dialog;
	public float textSpeed; // In words per min
	public float blinkSpeed; // Higher is faster

	private int line;
	private IEnumerator textCoroutine;
	private IEnumerator indicatorCoroutine;

	// Use this for initialization
	void Start () {
		line = 0;
		moreTextIndicator.enabled = false;
		textCoroutine = DisplayLine ();
		StartCoroutine(textCoroutine);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Submit")) {
			StopCoroutine (textCoroutine);
			StopCoroutine (indicatorCoroutine);
			moreTextIndicator.enabled = false;
			line++;

			if (line < dialog.Length) {
				textCoroutine = DisplayLine ();
				StartCoroutine (textCoroutine);
			}
		}
	}

	IEnumerator BlinkMoreTextIndicator() {
		while(true) {
			moreTextIndicator.enabled = !moreTextIndicator.enabled;
			yield return new WaitForSeconds (1.0f / blinkSpeed);
		}
	}

	IEnumerator DisplayLine() {
		for (int i = 0; i <= dialog[line].Length; i++) {
			uiText.text = dialog [line].Substring (0, i);
			yield return new WaitForSeconds (12.0f/textSpeed); // 60s / (speed * 5 letters per word)
		}

		if (line + 1 < dialog.Length) {
			indicatorCoroutine = BlinkMoreTextIndicator ();
			StartCoroutine (indicatorCoroutine);
		}
	}
}
