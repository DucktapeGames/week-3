using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float walkSpeed;
     private float curSpeed;
     private float maxSpeed;
 
     public float speed;
	 public float agility;
 
     void Start()
     {
         walkSpeed = (float)(speed + (agility/5));
         maxSpeed = walkSpeed + (walkSpeed / 2);
     }
 
     void FixedUpdate()
     {
         curSpeed = walkSpeed;
         maxSpeed = curSpeed;
 
         // Move senteces
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal")* curSpeed, 0.8f),
                                                Mathf.Lerp(0, Input.GetAxis("Vertical")* curSpeed, 0.8f));
 }
	
	void CheckForHit() {
		RaycastHit objectHit;

		Vector3 fwd = transform.TransformDirection(Vector3.up);
		Debug.DrawRay(transform.position, fwd, Color.green);
		if (Physics.Raycast(transform.position, fwd, out objectHit, 1))
		{
			//do something if hit object ie
			if(objectHit.transform.gameObject.tag == "Container") {
				Damageable dmg = objectHit.transform.gameObject.GetComponent<Damageable>();
				dmg.Damage(10);
			}
		}
	}
}
