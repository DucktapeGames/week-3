using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : MonoBehaviour {


	void Start(){
		Eye_Simulation.foundPlayer += Test; 
		Proximity_Sensing.lostPlayer += Test2; 
	}

	void Test(){

		Debug.Log ("in pursuit"); 
	}
		
	void Test2(){

		Debug.Log ("lost him"); 
	}


}
