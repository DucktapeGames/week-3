using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	public float speed = 5.0f;
	public bool moving = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
			moving = true;
		}

		if(Input.GetKey(KeyCode.W) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.D) != true) {
			moving = false;
		}
	}
}
