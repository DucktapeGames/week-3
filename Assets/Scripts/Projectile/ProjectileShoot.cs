using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour {

	// Use this for initialization
	public Rigidbody2D projectilePrefab;
	Rigidbody2D[] projectiles = new Rigidbody2D[20];
	Rigidbody2D currPrefab;
	public float fireSpeed = 0.5f;
	public float projectileSpeed = 50;
	public float coolDown;
	Vector3 pos; 
	int projectileCounter;
	void Start () {
		projectileCounter = 0;
		pos = transform.position;
		for(int i = 0; i < projectiles.Length; i++) {
			projectiles[i] = Instantiate(projectilePrefab, new Vector3(-100, -100, 0), Quaternion.identity) as Rigidbody2D;
		}
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
		if(projectileCounter > 19) {
			projectileCounter = 0;
		}
		
		projectiles[projectileCounter].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		projectiles[projectileCounter].transform.rotation = Quaternion.identity;
		projectiles[projectileCounter].AddForce(transform.up * projectileSpeed);
		coolDown = Time.time + fireSpeed;
		projectileCounter++;
	}
}
