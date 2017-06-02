using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {
	public DialogManager manager;
	public SpriteRenderer pressAlert;
	public bool triggerOnce;
	public bool pressToTrigger;
	public int dialogId;

	private bool triggered;
	private bool triggerStay;

	void Start() {
		triggered = false;
		pressAlert.enabled = false;
	}

	void Update() {
		if(triggerStay) {
			if(pressToTrigger) {
				if(Input.GetButtonDown("Action")) {
					Trigger();
				}
			}
		}
	}

	void Trigger() {
		if(!triggerOnce || !triggered) {
			manager.StartDialog(dialogId);
			triggered = true;
			pressAlert.enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Player") {
			if(!pressToTrigger) {
				Trigger();
			} else {
				if(!triggerOnce || !triggered) {
					pressAlert.enabled = true;
					triggerStay = true;
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			if(pressToTrigger) {
				if(!triggerOnce || !triggered) {
					pressAlert.enabled = false;
					triggerStay = false;
				}
			}
		}
	}
}
