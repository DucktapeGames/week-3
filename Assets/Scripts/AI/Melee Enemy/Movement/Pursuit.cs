using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : MonoBehaviour {


	private Coroutine currentRoutine; 



	void Start(){
		Eye_Simulation.foundPlayer += resumePursuit; 
		Proximity_Sensing.lostPlayer += pausePursuit; 
	}

	IEnumerator pursuit(){
		while (true) {
			Debug.Log ("in pursuit"); 
			yield return new WaitForSeconds(0.04f); 
		}
	}

	void resumePursuit(){
		currentRoutine = null; 
		currentRoutine = StartCoroutine (pursuit ()); 
	}
		
	void pausePursuit(){

		Debug.Log ("lost him"); 
		StopCoroutine (currentRoutine); 
	}


}
