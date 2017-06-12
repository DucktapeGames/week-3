using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour {

	private Animator anim; 
	private Rigidbody player; 

	// Use this for initialization
	void Awake () {
		anim = GameObject.FindGameObjectWithTag ("Player2D").GetComponent<Animator> (); 
		player = this.gameObject.GetComponent<Rigidbody> (); 
	}


	void Update(){
		if (player.velocity.magnitude >0.01f) {
			anim.SetBool ("moving", true); 
		} else {
				anim.SetBool ("moving", false); 
		}
	}
}
