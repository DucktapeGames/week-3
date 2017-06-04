using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : MonoBehaviour {


	private Coroutine currentRoutine; 
	public Transform playerTrans; 
	[SerializeField][Range(0,360)]
	public float enemyTurnVelocity; 
	[SerializeField][Range(0,100)]
	public float enemyVelocity;
	[SerializeField][Range(0,10)]
	public float offsetDistanceFromTargetSoItDoesNotOverlap; 
	//public Rigidbody2D rbody; 
	//raycasting
	RaycastHit2D hit; 
	//pathfinding
	private Grid grid; 
	private Vector3 directionToPlayer; 


	void Awake(){
		//rbody = this.GetComponent<Rigidbody2D> (); 
		playerTrans = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
	    grid = GameObject.FindGameObjectWithTag ("PathFindingManager").GetComponent<Grid> ();
		directionToPlayer = this.transform.position; 

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
			if (Vector3.Distance (transform.position, playerTrans.position) > offsetDistanceFromTargetSoItDoesNotOverlap
				&& grid.path != null && grid.path.Count>0) {
				directionToPlayer = new Vector3 (grid.path [0].worldPosition.x, grid.path [0].worldPosition.y, 0);
				this.transform.position = Vector3.Lerp (this.transform.position, directionToPlayer, enemyVelocity * Time.fixedDeltaTime);
			} 
			yield return new WaitForSeconds(0.04f); 
		}
	}
	public void resumePursuit(){
		currentRoutine = null; 
		currentRoutine = StartCoroutine (pursuit ()); 
	}
		
	public void pausePursuit(){

		Debug.Log ("lost him"); 
		if (currentRoutine != null) {
			StopCoroutine (currentRoutine); 
		}
	}


}
