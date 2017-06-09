using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCursor : MonoBehaviour {
	Vector3 mousePostion;
	Camera cam;
	Rigidbody2D rid;

	// Use this for initialization
	void Start () {
		rid = GetComponent<Rigidbody2D>();
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		RotateToCamera();
	}

	void RotateToCamera() {
		mousePostion = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
		rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePostion.y - transform.position.y), (mousePostion.x - transform.position.x)) * Mathf.Rad2Deg - 90);
	}
}
