using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	public delegate void LevelGoals(); 
	public static event LevelGoals FinishedLevel; 

	void OnTriggerEnter2D(Collider2D something){
		Debug.Log ("hey"); 
		if (something.gameObject.tag == "Player2D") {
			FinishedLevel (); 
			Destroy (this.gameObject); 
		}
	}


}
