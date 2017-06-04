using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proximity_Sensing : MonoBehaviour {
	

	//references
	private Eye_Simulation eyeSimulationReference; 
	private Pursuit pursuitScriptReferece; 
	private PathFinding pathfindingScriptReferece; 
	private Patrolling patrolScriptReferece; 


	void Awake(){
		eyeSimulationReference = this.gameObject.GetComponentInParent<Eye_Simulation> (); 
		pursuitScriptReferece = this.gameObject.GetComponentInParent<Pursuit> (); 
		pathfindingScriptReferece = GameObject.FindGameObjectWithTag ("PathFindingManger").GetComponent<PathFinding> ();
		patrolScriptReferece = this.gameObject.GetComponentInParent<Patrolling> (); 
	}


	void OnTriggerEnter2D(Collider2D something){
		if (something.tag == "Player") {
			eyeSimulationReference.resumeLookForPlayer (); 
			patrolScriptReferece.pauseResturnToPatrolPosition (); 
		}
	}

	void OnTriggerExit2D(Collider2D something){
		if (something.tag == "Player") {
			eyeSimulationReference.pauseLookForPlayer (); 
			pursuitScriptReferece.pausePursuit (); 
			pathfindingScriptReferece.pauseGetPath ();
			patrolScriptReferece.resumeReturnToPatrolPosition (); 
		}
	}


}
