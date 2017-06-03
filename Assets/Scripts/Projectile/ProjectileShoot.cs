using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour {

	// Use this for initialization
	public Rigidbody2D projectilePrefab;
	public float fireSpeed = 0.5f;
	public float projectileSpeed = 50;
	public float coolDown;
	public float xVal;
	public float yVal;
	Vector3 pos; 
	void Start () {
		pos = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		if(Input.GetKey(KeyCode.A) && transform.position == pos) {        // Left
			pos += Vector3.left;
		}
		if(Input.GetKey(KeyCode.D) && transform.position == pos) {        // Right
			pos += Vector3.right;
		}
		if(Input.GetKey(KeyCode.W) && transform.position == pos) {        // Up
			pos += Vector3.up;
		}
		if(Input.GetKey(KeyCode.S) && transform.position == pos) {        // Down
			pos += Vector3.down;
		}
	}
	void Update () {
		if(Time.time >= coolDown)
		{
			if(Input.GetKey(KeyCode.K))
			{
				Fire();
			}
		}
	}

	void Fire()
	{
		Rigidbody2D pPrefab = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as Rigidbody2D;
		pPrefab.AddForce(transform.up * projectileSpeed);
		coolDown = Time.time + fireSpeed;
	}
}
