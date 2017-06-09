using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
	public GameObject  player;
	bool followPlayer = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(followPlayer) {
			CamFollowPlayer();
		}
	}

	void SetFollowPlayer(bool val) {
		followPlayer = val;
	}

	void CamFollowPlayer() {
		Vector3 newPos = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
		transform.position = newPos;
	}
}
