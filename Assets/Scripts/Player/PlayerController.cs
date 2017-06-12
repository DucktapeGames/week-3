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

	bool moving;

	private RotateToCursor rotateScript; 

	void Awake() {
		rotateScript = this.gameObject.GetComponent<RotateToCursor> (); 
		rbody = this.gameObject.GetComponent<Rigidbody> (); 
		anim = GameObject.FindGameObjectWithTag ("Player2D").GetComponent<Animator>();
	}
 
     void Start()
     {
		time = 0; 
        walkSpeed = (float)(speed + (agility/5));
		moving = false;
        // maxSpeed = walkSpeed + (walkSpeed / 2);
     }

	void Update() {
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
		if(Input.GetKey(KeyCode.W)) {
			transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if(Input.GetKey(KeyCode.A)) {
			transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if(Input.GetKey(KeyCode.S)) {
			transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
			moving = true;
		}
		if(Input.GetKey(KeyCode.D)) {
			transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
			moving = true;
		}

		if(Input.GetKey(KeyCode.W) != true && Input.GetKey(KeyCode.A) != true && Input.GetKey(KeyCode.S) != true && Input.GetKey(KeyCode.D) != true) {
			moving = false;
		}

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
