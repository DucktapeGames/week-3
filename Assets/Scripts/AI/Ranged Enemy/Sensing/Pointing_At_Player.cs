using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointing_At_Player : MonoBehaviour {

	//public customizable prefab variables
	[SerializeField][Range(0,20)]
	public float ViewRange; 
	[SerializeField][Range(0,360)]
	public float ViewAngle; 
	[SerializeField][Range(1,100)]
	public float Quality; 

	//private member variables
	private Transform player2D; 

	void Awake(){
		player2D = GameObject.FindGameObjectWithTag ("Player2D").transform; 
	}


	public Vector2 DirFromAngle(float angleInDegrees, bool isGlobal){
		if (!isGlobal) {
			angleInDegrees += transform.eulerAngles.z; 
		}
		return new Vector2 (Mathf.Sin(-angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(-angleInDegrees * Mathf.Deg2Rad));
	}

	IEnumerator TrackPlayerMovement(){
		while (true) {

			yield return new WaitForSeconds(1/Quality); 
		}
	}

}
