using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float walkSpeed;
     private float curSpeed;
     //private float maxSpeed;
 
     public float speed;
	 public float agility;
	public float range; 
	private Rigidbody rbody;
	private Transform player2d; 

	private RotateToCursor rotateScript; 

	void Awake(){
		player2d = GameObject.FindGameObjectWithTag ("Player2D").transform; 
		rotateScript = this.gameObject.GetComponent<RotateToCursor> (); 
		rbody = this.gameObject.GetComponent<Rigidbody> (); 
	}
 
     void Start()
     {
         walkSpeed = (float)(speed + (agility/5));
        // maxSpeed = walkSpeed + (walkSpeed / 2);
     }
 
     void FixedUpdate()
     {
         curSpeed = walkSpeed;
         //maxSpeed = curSpeed;
 
         // Move senteces
		rbody.velocity = (Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f) * Vector3.Cross(Vector3.up, rotateScript.Direction.normalized))+ 
			(Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f) * rotateScript.Direction.normalized);
		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			CheckForHit (); 
		}
	}
	
	void CheckForHit() {
		RaycastHit objectHit;

		Vector3 fwd = rotateScript.Direction.normalized; 
		Debug.DrawLine(transform.position, transform.position +  (fwd * range), Color.green, 5f);
		if (Physics.Raycast (transform.position, fwd, out objectHit, range))
		{
			//do something if hit object ie
			if(objectHit.transform.gameObject.tag == "Container") {
				Damageable dmg = objectHit.transform.gameObject.GetComponent<Damageable>();
				dmg.Damage(10);
			}
		}
	}
}
