using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing : MonoBehaviour {

	private Animator anim; 
	private PathFinding pathfinding; 
	private Coroutine currentRoutine; 

	void Awake(){
		anim = this.gameObject.GetComponentInChildren<Animator> (); 
		pathfinding = this.gameObject.GetComponent<PathFinding> (); 
	}

	void Start(){
		currentRoutine = StartCoroutine (animate ()); 
	}

	IEnumerator animate(){
		while (true) {
			anim.SetBool ("moving", pathfinding.moving); 
			anim.SetBool ("attacking", pathfinding.attacking); 
			yield return new WaitForSeconds (0.2f); 
		}
	}

	void PauseAnimation(){
		if (currentRoutine != null) {
			StopCoroutine (currentRoutine); 
			currentRoutine = null; 
		}
	}


}
