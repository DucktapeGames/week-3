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
	public float attackDelay; 
	private float time; 
	private Rigidbody rbody;
	private Animator anim; 

	private RotateToCursor rotateScript; 

	void Awake(){
		rotateScript = this.gameObject.GetComponent<RotateToCursor> (); 
		rbody = this.gameObject.GetComponent<Rigidbody> (); 
		anim = GameObject.FindGameObjectWithTag ("Player2D").GetComponent<Animator>(); 
	}
 
     void Start()
     {
		time = 0; 
         walkSpeed = (float)(speed + (agility/5));
        // maxSpeed = walkSpeed + (walkSpeed / 2);
     }

	void Update(){
		if (time <= attackDelay) {
			time += Time.fixedDeltaTime; 
		} else {
			anim.SetBool ("attacking", false); 
		}
	}
 
     void FixedUpdate()
     {
         curSpeed = walkSpeed;
         //maxSpeed = curSpeed;
 
         // Move senteces
		rbody.velocity = (Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f) * Vector3.Cross(Vector3.up, rotateScript.Direction.normalized))+ 
			(Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f) * rotateScript.Direction.normalized);
		if (Input.GetKeyDown(KeyCode.Mouse0) && time >= attackDelay) {
			CheckForHit (); 
			time = 0; 
			anim.SetBool ("attacking", true); 
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
				dmg.Damage(30);
			}
		}
	}
}
