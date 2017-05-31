using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	// Use this for initialization
	Vector3 pos;                                // For movement
	public float speed;                         // Speed of movement
	Vector3 moveDirection;
	float angle;
		
	void Start () {
		pos = transform.position;          // Take the initial position
	}

	void FixedUpdate () {
		if(Input.GetKey(KeyCode.A) && transform.position == pos) {        // Left
			pos += Vector3.left;
		}
		if(Input.GetKey(KeyCode.D) && transform.position == pos) {        // Right
			pos += Vector3.right;
		}
		if(Input.GetKey(KeyCode.W) && transform.position == pos) {        // Up
			pos += Vector3.up;
		}
		if(Input.GetKey(KeyCode.S) && transform.position == pos) {        // Down
			pos += Vector3.down;
		}
		transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * speed);    // Move there
	}

	 void Update()
	{
		moveDirection = gameObject.transform.position - pos; 
		if (moveDirection != Vector3.zero) 
		{
			angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
	}
}
