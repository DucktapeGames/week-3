using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {
	public DialogManager manager;
	public int dialogId;

	void OnTriggerEnter2D(Collider2D other) {
		manager.StartDialog(dialogId);
	}
}
