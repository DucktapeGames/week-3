using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {



	public delegate void Ending (); 
	public static event Ending loadEnding; 

	void OnTriggerEnter2D(Collider2D something){
		if (something.tag == "Player2D") {
			loadEnding (); 
		}
	}
}
