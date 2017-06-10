using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCursor : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		RotateToCamera();
	}

	void RotateToCamera() {
		Vector3 dir = Input.mousePosition - transform.position;
		float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.up); 
	}
}
