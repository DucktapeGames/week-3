using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogBox : MonoBehaviour {
	public Text uiText;
	public Image uiImage;
	public Image moreTextIndicator;
	public string[] dialog;
	public float textSpeed; // In words per min
	public float blinkSpeed; // Higher is faster
	public UnityEvent dialogCompleted;

	private int line;
	private IEnumerator textCoroutine;
	private IEnumerator indicatorCoroutine;

	public void LoadLineWithCallback(Line l, UnityAction callback) {
		dialog = l.message;
		uiImage.sprite = Resources.Load<Sprite>("DialogImages/" + l.image);
		dialogCompleted.RemoveAllListeners();
		dialogCompleted.AddListener(callback);
		Reset();
	}

	public void Reset() {
		line = 0;

		if(textCoroutine != null) {
			StopCoroutine(textCoroutine);
		}

		if(indicatorCoroutine != null) {
			StopCoroutine(indicatorCoroutine);
		}
		
		moreTextIndicator.enabled = false;
		textCoroutine = DisplayLine ();

		StartCoroutine(textCoroutine);
	}

	// Use this for initialization
	void Start() {
		Reset();
	}
	
	// Update is called once per frame
	void Update() {
		if(Input.GetButtonDown("Submit")) {
			StopCoroutine(textCoroutine);
			StopCoroutine(indicatorCoroutine);
			moreTextIndicator.enabled = false;
			line++;

			if (line < dialog.Length) {
				textCoroutine = DisplayLine ();
				StartCoroutine(textCoroutine);
			} else {
				dialogCompleted.Invoke();
			}
		}
	}

	// Coroutine that blinks the more text indicator
	IEnumerator BlinkMoreTextIndicator() {
		while(true) {
			moreTextIndicator.enabled = !moreTextIndicator.enabled;
			yield return new WaitForSeconds(1.0f / blinkSpeed);
		}
	}

	// Coroutine that displays the text in a dialog fashon (one letter at a time)
	IEnumerator DisplayLine() {
		var tags = new List<string>();
		var currentLine = dialog[line];

		// Looks for rich text tags
		for (int i = 0; i < dialog[line].Length; i++) {
			if(currentLine[i].Equals('<')) {
				i++;
				if(currentLine[i].Equals('/')) {
					i++;
					int length = 0;
					while(!currentLine[i + length].Equals('>')) {
						length++;
					}

					tags.Remove(currentLine.Substring(i, length));
					i += length;
				} else {
					int length = 0;
					while(!currentLine[i + length].Equals('>')) {
						length++;
					}

					tags.Add(currentLine.Substring(i, length));

					i += length;
				}
			}

			var substring = currentLine.Substring(0, i + 1);

			// Appends rich text closing tags, if any.
			for(int j = tags.Count - 1; j >= 0; j--) {
				substring += "</" + tags[j] + ">";
			}

			uiText.text = substring;
			yield return new WaitForSeconds(12.0f / textSpeed); // 60s / (speed * 5 letters per word)
		}

		if (line + 1 < dialog.Length) {
			indicatorCoroutine = BlinkMoreTextIndicator();
			StartCoroutine(indicatorCoroutine);
		}
	}
}
