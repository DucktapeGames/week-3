using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCursor : MonoBehaviour {

	[SerializeField][Range(1,100)]
	public float Quality; 

	private Vector3 dir; 
	public Vector3 Direction{
		get{
			return dir - this.transform.position; 
		}
	}
	// Use this for initialization
	void Start () {
		StartCoroutine (DoRotateCamera ()); 
	}
	
	// Update is called once per frame
	IEnumerator DoRotateCamera () {
		while(true){
			RotateToCamera();
			yield return new WaitForSeconds(2/Quality); 
		}
	}

	void RotateToCamera() {
		dir = Camera.main.ScreenToWorldPoint (Input.mousePosition); 
		dir = new Vector3 (dir.x, 1, dir.y + 24); 
		this.transform.rotation = Quaternion.LookRotation (dir - this.transform.position, Vector3.up);  
	}
}
