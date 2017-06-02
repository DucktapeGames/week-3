using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour {

	public GameObject dialogBoxPrefab;
	public Canvas userInterface;
	public TextAsset Dialogs;

	private Dictionary<int, Line[]> dialogsMap;
	private DialogBox dialogBox;
	private int lineNumber;
	private Line[] activeDialog;
	private UnityAction lineCompletedCallback;

	// Use this for initialization
	void Start () {
		lineCompletedCallback = new UnityAction(LineCompleted);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartDialog(int dialogId) {
		if(dialogBox == null) {
			dialogBox = Instantiate(dialogBoxPrefab, userInterface.transform)
				.GetComponent<DialogBox>();
		}
		
		lineNumber = 0;
		activeDialog = dialogsMap[dialogId];

		dialogBox.LoadLineWithCallback(activeDialog[lineNumber], lineCompletedCallback);
	}

	private void LineCompleted() {
		lineNumber++;

		if(lineNumber < activeDialog.Length) {
			dialogBox.LoadLineWithCallback(activeDialog[lineNumber], lineCompletedCallback);
		} else {
			Destroy(dialogBox);
		}
	}
}
