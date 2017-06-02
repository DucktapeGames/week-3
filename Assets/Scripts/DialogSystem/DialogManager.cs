using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour {
	
	public GameObject dialogBoxPrefab;
	public Canvas userInterface;
	public TextAsset dialogsFile;

	private Dictionary<int, Line[]> dialogsMap;
	private DialogBox dialogBox;
	private int lineNumber;
	private Line[] activeDialog;
	private UnityAction lineCompletedCallback;

	// Use this for initialization
	void Start () {
		lineCompletedCallback = new UnityAction(LineCompleted);

		dialogsMap = new Dictionary<int, Line[]>();
		
		var json = dialogsFile.text;
		var dialogsList = JsonUtility.FromJson<DialogList>(json);

		foreach(Dialog d in dialogsList.dialogs) {
			dialogsMap.Add(d.id, d.lines);
		}
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
			Destroy(dialogBox.gameObject);
		}
	}
}

//Disable null dialogs warning
#pragma warning disable 0649
[System.Serializable]
class DialogList {
	public Dialog[] dialogs;
}
#pragma warning restore 0649
