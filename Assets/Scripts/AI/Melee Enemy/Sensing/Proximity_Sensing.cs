using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity_Sensing : MonoBehaviour {
	
	//events
	public delegate void PlayerDetection(); 
	public static event PlayerDetection lostPlayer, sensingPlayer; 

	void Start(){
		
	}

	void OnTriggerEnter2D(Collider2D something){
		if (something.tag == "Player") {
			sensingPlayer (); 
		}
	}

	void OnTriggerExit2D(Collider2D something){
		if (something.tag == "Player") {
			lostPlayer (); 
		}
	}


}
