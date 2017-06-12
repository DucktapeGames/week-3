using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	private SpriteRenderer door2d; 
	public Sprite OpenedDoorSprite; 

	void Awake(){
		Key.FinishedLevel += Open; 
		door2d = GameObject.FindGameObjectWithTag ("Door2D").GetComponent<SpriteRenderer> (); 
	}

	public void Open(){
		door2d.sprite = OpenedDoorSprite; 
		Destroy (this.gameObject); 
	}




}
