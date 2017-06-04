using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour {

	private Grid grid; 
	private Vector3 originalPosition;
	private Quaternion originalRotation; 
	private Pursuit referenceValues; 
	private Vector3 targetDirection; 
	private PathFinding referencePathfinding;  
	private Coroutine currentRoutine; 


	void Awake(){
		grid = GameObject.FindGameObjectWithTag ("PathFindingManager").GetComponent<Grid> ();
		originalPosition = this.transform.position; 
		originalRotation = this.transform.rotation; 
		referenceValues = this.gameObject.GetComponent<Pursuit> ();
		referencePathfinding = this.gameObject.GetComponent<PathFinding> ();
	}


	IEnumerator returnToPatrolSpot(){
		referencePathfinding.FindPath (this.transform.position, originalPosition); 
		int PathNodeNUmber = grid.path.Count; 
		int PathIndex = 0; 
		while (true) { 
			this.transform.rotation = Quaternion.Lerp (this.transform.rotation, originalRotation, referenceValues.enemyTurnVelocity * Time.fixedUnscaledDeltaTime);
			if (Vector3.Distance (transform.position, originalPosition) > 0
				&& grid.path != null && PathIndex<PathNodeNUmber) {
				targetDirection = new Vector3 (grid.path [PathIndex].worldPosition.x, grid.path [PathIndex].worldPosition.y, 0);
				this.transform.position = Vector3.Lerp (this.transform.position, targetDirection, referenceValues.enemyVelocity * Time.fixedDeltaTime);
			}
			if(Vector3.Distance (transform.position, targetDirection) < 0.3f && PathIndex+1<PathNodeNUmber){
				PathIndex++; 
				//Debug.Log ("Paht Index");
			}
			yield return new WaitForSeconds(0.04f); 
		}
	}


	public void resumeReturnToPatrolPosition(){
		currentRoutine = null; 
		currentRoutine = StartCoroutine (returnToPatrolSpot ()); 

	}

	public void pauseResturnToPatrolPosition(){
		if(currentRoutine!=null){
			StopCoroutine (currentRoutine); 
		}
	}


}
