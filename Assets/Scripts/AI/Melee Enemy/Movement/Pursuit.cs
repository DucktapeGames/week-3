using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : MonoBehaviour {


	private Coroutine currentRoutine; 
	public Transform playerTrans; 
	[SerializeField][Range(0,100)]
	public float distanceFromPlayerOffSetSoItDoesNotOverlap;
	[SerializeField][Range(0,360)]
	public float enemyTurnVelocity; 
	[SerializeField][Range(0,100)]
	public float enemyVelocity;
	[SerializeField][Range(0,5)]
	public float detectionDistance;
	//raycasting
	RaycastHit2D hit; 
	//bs
	private Vector2 directionToPlayer; 


	void Awake(){
		//rbody = this.GetComponent<Rigidbody2D> (); 
		playerTrans = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
	}

	void Start(){
		Eye_Simulation.foundPlayer += resumePursuit; 
		Proximity_Sensing.lostPlayer += pausePursuit; 
	}

	IEnumerator pursuit(){ 
		while (true) {
			//right now it only looks at player. 
			if (Vector2.Angle (new Vector2 (-transform.up.x, -transform.up.y), new Vector2 (playerTrans.position.x, playerTrans.position.y)) > 10) {
				if (Vector3.Cross((playerTrans.position - this.transform.position), -this.transform.up).z <0) { 
					this.transform.RotateAround (transform.position, Vector3.forward, enemyTurnVelocity * Time.fixedDeltaTime); 
				} else {
					this.transform.RotateAround (transform.position, Vector3.forward, -enemyTurnVelocity * Time.fixedDeltaTime); 
				}
			}

			yield return new WaitForSeconds(0.04f); 
		}
	}
	void resumePursuit(){
		currentRoutine = null; 
		currentRoutine = StartCoroutine (pursuit ()); 
	}
		
	void pausePursuit(){

		Debug.Log ("lost him"); 
		StopCoroutine (currentRoutine); 
	}


}
