using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	// Use this for initialization
	public GameObject[] items = new GameObject[3];
	
	public void Spawn() {
		Vector3 position = transform.position;
		int itemPos = Random.Range(0, items.Length);
         // Clone the objects that are "in" the box.
		 Instantiate(items[itemPos], position, Quaternion.identity);
         // Get rid of the box.
         Destroy(gameObject);
	}
}
